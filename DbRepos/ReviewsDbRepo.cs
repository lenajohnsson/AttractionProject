
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos;

public class ReviewsDbRepo
{
    readonly MainDbContext _context;
    ILogger<ReviewsDbRepo> _logger;

    public ReviewsDbRepo(MainDbContext context,
                            ILogger<ReviewsDbRepo> logger)
    {
        _context = context;
        _logger = logger;
    }
}