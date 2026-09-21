
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
}