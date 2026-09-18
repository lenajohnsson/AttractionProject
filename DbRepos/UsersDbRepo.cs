
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

    public async Task<ResponsePageDto<IUser>> ReadUsersAsync(int pageNr, int pageSz)
    {
        IQueryable<UserDbM> query = _dbContext.Users
            .Include(i => i.ReviewsDbM);

        var ret = new ResponsePageDto<IUser>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
                .Where(i => i.ReviewsDbM.Any()).CountAsync(),

            PageItems = await query
                .Where(i => i.ReviewsDbM.Any())
                .Skip(pageNr * pageSz)
                .Take(pageSz)
                .ToListAsync<IUser>(),

            PageNr = pageNr,
            PageSize = pageSz
        };
        return ret;
    }
}