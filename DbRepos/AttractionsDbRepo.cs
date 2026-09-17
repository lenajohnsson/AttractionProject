
using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace DbRepos;

public class AttractionsDbRepo
{
    readonly MainDbContext _dbContext;
    ILogger<AttractionsDbRepo> _logger;

    public AttractionsDbRepo(MainDbContext context,
                            ILogger<AttractionsDbRepo> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    public async Task<ResponsePageDto<IAttraction>> ReadAttractionsAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz)
    {
        if (filter == null)
            filter = "";

        IQueryable<AttractionDbM> query;

        if (flat)
        {
            query = _dbContext.Attractions;
        }
        else
        {
            query = _dbContext.Attractions
                .Include(i => i.AddressDbM)
                .Include(i => i.CategoriesDbM);
        }

        var ret = new ResponsePageDto<IAttraction>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
            .Where(i => (i.Seeded == seeded) &&
                        (i.AttractionName.ToLower().Contains(filter) ||
                        i.Description.ToLower().Contains(filter) ||
                        i.CategoriesDbM.Any(c => c.CategoryType.ToLower().Contains(filter)) ||
                        i.AddressDbM.Country.ToLower().Contains(filter) ||
                        i.AddressDbM.City.ToLower().Contains(filter))).CountAsync(),

            PageItems = await query
            .Where(i => (i.Seeded == seeded) &&
                        (i.AttractionName.ToLower().Contains(filter) ||
                        i.Description.ToLower().Contains(filter) ||
                        i.CategoriesDbM.Any(c => c.CategoryType.ToLower().Contains(filter)) ||
                        i.AddressDbM.Country.ToLower().Contains(filter) ||
                        i.AddressDbM.City.ToLower().Contains(filter)))
            .Skip(pageNr * pageSz)
            .Take(pageSz)
            .ToListAsync<IAttraction>(),

            PageNr = pageNr,
            PageSize = pageSz
        };
        return ret;
    }

    public async Task<ResponsePageDto<IAttraction>> ReadAttractionsWithoutReviewAsync(int pageNr, int pageSz)
    {
        IQueryable<AttractionDbM> query = _dbContext.Attractions
            .Include(i => i.ReviewsDbM);

        var ret = new ResponsePageDto<IAttraction>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
                .Where(i => !i.ReviewsDbM.Any()).CountAsync(),

            PageItems = await query
                .Where(i => !i.ReviewsDbM.Any())
                .Skip(pageNr * pageSz)
                .Take(pageSz)
                .ToListAsync<IAttraction>(),

            PageNr = pageNr,
            PageSize = pageSz
        };
        return ret;
    }

    public async Task<ResponseItemDto<IAttraction>> ReadAttractionAsync(Guid id, bool flat)
    {
        IAttraction item;
        if (flat)
        {
            var query = _dbContext.Attractions
                .Where(i => i.AttractionId == id);

            item = await query.FirstOrDefaultAsync<IAttraction>();
        }
        else
        {
            var query = _dbContext.Attractions
                .Include(i => i.CategoriesDbM)
                .Include(i => i.AddressDbM)
                .Include(i => i.ReviewsDbM)
                .Where(i => i.AttractionId == id);

            item = await query.FirstOrDefaultAsync<IAttraction>();
        }

        if (item == null) throw new ArgumentException($"Item {id} does not exist");

        return new ResponseItemDto<IAttraction>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item

        };
    }
}