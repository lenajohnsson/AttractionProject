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

    public async Task<ResponsePageDto<IAddress>> ReadAddressesAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz)
    {
        if (filter == null)
            filter = "";

        IQueryable<AddressDbM> query;

        if (flat)
        {
            query = _dbContext.Addresses;
        }
        else
        {
            query = _dbContext.Addresses
                .Include(i => i.AttractionsDbM)
                .ThenInclude(i => i.CategoriesDbM)
                .Include(i => i.AttractionsDbM)
                .ThenInclude(i => i.ReviewsDbM);
        }

        var ret = new ResponsePageDto<IAddress>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            // Counting rows, filtered or not
            DbItemsCount = await query
            .Where(i => (i.Seeded == seeded) &&
                        (i.StreetAddress.ToLower().Contains(filter) ||
                            i.City.ToLower().Contains(filter) ||
                            i.Country.ToLower().Contains(filter))).CountAsync(),

            // Listing objects, filtered or not
            PageItems = await query
            .Where(i => (i.Seeded == seeded) &&
                        (i.StreetAddress.ToLower().Contains(filter) ||
                            i.City.ToLower().Contains(filter) ||
                            i.Country.ToLower().Contains(filter)))
            // Pagination
            .Skip(pageNr * pageSz)
            .Take(pageSz)
            .ToListAsync<IAddress>(),

            PageNr = pageNr,
            PageSize = pageSz
        };
        return ret;
    }
}