
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

    public async Task<ResponsePageDto<IUser>> ReadUsersAsync()
    {
        IQueryable<UserDbM> query = _dbContext.Users;
        var ret = new ResponsePageDto<IUser>()
        {
            ConnectionString = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query.ToListAsync<IUser>()
        };
        return ret;
    }
}