
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
                .Include(i => i.CategoriesDbM)
                .Include(i => i.ReviewsDbM);
        }

        var ret = new ResponsePageDto<IAttraction>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
            .Where(i => (i.Seeded == seeded) &&
                        i.AttractionName.ToLower().Contains(filter)).CountAsync(),

            PageItems = await query
            .Where(i => (i.Seeded == seeded) &&
                        i.AttractionName.ToLower().Contains(filter))
            .Skip(pageNr * pageSz)
            .Take(pageSz)
            .ToListAsync<IAttraction>(),

            PageNr = pageNr,
            PageSize = pageSz
        };
        return ret;
    }
}