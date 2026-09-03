
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos;

public class CategoriesDbRepo
{
    readonly MainDbContext _context;
    ILogger<CategoriesDbRepo> _logger;

    public CategoriesDbRepo(MainDbContext context,
                            ILogger<CategoriesDbRepo> logger)
    {
        _context = context;
        _logger = logger;
    }
}