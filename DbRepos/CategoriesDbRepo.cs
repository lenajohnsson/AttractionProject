using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace DbRepos;

public class CategoriesDbRepo
{
    readonly MainDbContext _dbContext;

    public CategoriesDbRepo(MainDbContext context)
    {
        _dbContext = context;
    }

    public async Task<ResponsePageDto<ICategory>> ReadCategoriesAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz)
    {
        if (filter == null)
            filter = "";

        IQueryable<CategoryDbM> query;

        if (flat)
        {
            query = _dbContext.Categories;
        }
        else
        {
            query = _dbContext.Categories
                .Include(i => i.AttractionsDbM)
                .ThenInclude(i => i.AddressDbM)
                .Include(i => i.AttractionsDbM)
                .ThenInclude(i => i.ReviewsDbM);
        }

        return new ResponsePageDto<ICategory>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
            .Where(i => (i.Seeded == seeded) &&
                        i.CategoryType.ToLower().Contains(filter)).CountAsync(),

            PageItems = await query
            .Where(i => (i.Seeded == seeded) &&
                        i.CategoryType.ToLower().Contains(filter))
            .Skip(pageNr * pageSz)
            .Take(pageSz)
            .ToListAsync<ICategory>(),

            PageNr = pageNr,
            PageSize = pageSz
        };
    }
}