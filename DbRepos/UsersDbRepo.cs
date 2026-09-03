
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos;

public class UsersDbRepo
{
    readonly MainDbContext _context;
    ILogger<UsersDbRepo> _logger;

    public UsersDbRepo(MainDbContext context,
                            ILogger<UsersDbRepo> logger)
    {
        _context = context;
        _logger = logger;
    }
}