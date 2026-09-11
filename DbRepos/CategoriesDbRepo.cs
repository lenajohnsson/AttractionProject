
using System.Reflection.Metadata.Ecma335;
using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace DbRepos;

public class CategoriesDbRepo
{
    readonly MainDbContext _dbContext;
    ILogger<CategoriesDbRepo> _logger;

    public CategoriesDbRepo(MainDbContext context,
                            ILogger<CategoriesDbRepo> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    public async Task<ResponsePageDto<ICategory>> ReadCategoriesAsync()
    {
        IQueryable<CategoryDbM> query = _dbContext.Categories;

        var ret = new ResponsePageDto<ICategory>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query.CountAsync(),
            PageItems = await query.ToListAsync<ICategory>()
        };
        return ret;
    }
}