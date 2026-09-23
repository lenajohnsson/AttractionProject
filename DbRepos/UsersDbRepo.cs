
using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace DbRepos;

public class UsersDbRepo
{
    readonly MainDbContext _dbContext;
    ILogger<UsersDbRepo> _logger;

    public UsersDbRepo(MainDbContext context,
                            ILogger<UsersDbRepo> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    public async Task<ResponsePageDto<UserReadDto>> ReadUsersAsync(int pageNr, int pageSz)
    {
        IQueryable<UserDbM> query = _dbContext.Users
            .Include(i => i.ReviewsDbM);

        var pageItems = await query
            .Where(i => i.ReviewsDbM.Any())
            .Skip(pageNr * pageSz)
            .Take(pageSz)
            .ToListAsync();

        var dtoItems = pageItems.Select(u => new UserReadDto
        {
            UserId = u.UserId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Reviews = u.Reviews?.Select(r => new ReviewReadDto
            {
                Comment = r.Comment,
                ReviewGrade = r.ReviewGrade,
                Date = r.Date
            }).ToList()
        }).ToList();

        var ret = new ResponsePageDto<UserReadDto>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
                .Where(i => i.ReviewsDbM.Any()).CountAsync(),

            PageItems = dtoItems,

            PageNr = pageNr,
            PageSize = pageSz
        };
        return ret;
    }

    public async Task<ResponseItemDto<IUser>> ReadUserAsync(Guid id, bool flat)
    {
        IUser item;

        if (flat)
        {
            var query = _dbContext.Users
                .Where(i => i.UserId == id);

            item = await query.FirstOrDefaultAsync<IUser>();
        }
        else
        {
            var query = _dbContext.Users
                .Include(i => i.ReviewsDbM)
                .Where(i => i.UserId == id);
            item = await query.FirstOrDefaultAsync<IUser>();
        }

        if (item == null)
            throw new ArgumentException($"Item {id} does not exist");

        return new ResponseItemDto<IUser>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }

    public async Task<ResponseItemDto<IUser>> CreateUserAsync(UserCuDto itemDto)
    {
        if (itemDto.UserId != null)
            throw new ArgumentException($"{nameof(itemDto.UserId)} must be null when creating a new user");

        var item = new UserDbM(itemDto);


        _dbContext.Users.Add(item);

        await _dbContext.SaveChangesAsync();

        return await ReadUserAsync(item.UserId, false);
    }


}