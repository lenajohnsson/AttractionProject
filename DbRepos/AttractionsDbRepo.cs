
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos;

public class AttractionsDbRepo
{
    readonly MainDbContext _context;
    ILogger<AttractionsDbRepo> _logger;

    public AttractionsDbRepo(MainDbContext context,
                            ILogger<AttractionsDbRepo> logger)
    {
        _context = context;
        _logger = logger;
    }
}