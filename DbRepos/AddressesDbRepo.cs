
using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace DbRepos;

public class AddressesDbRepo
{
    readonly MainDbContext _dbContext;
    ILogger<AddressesDbRepo> _logger;

    public AddressesDbRepo(MainDbContext context,
                            ILogger<AddressesDbRepo> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    public async Task<ResponsePageDto<IAddress>> ReadAddressesAsync()
    {
        IQueryable<AddressDbM> query = _dbContext.Addresses;
        var ret = new ResponsePageDto<IAddress>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query.CountAsync(),
            PageItems = await query.ToListAsync<IAddress>()
        };
        return ret;
    }
}