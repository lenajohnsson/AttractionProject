
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos;

public class AddressesDbRepo
{
    readonly MainDbContext _context;
    ILogger<AddressesDbRepo> _logger;

    public AddressesDbRepo(MainDbContext context,
                            ILogger<AddressesDbRepo> logger)
    {
        _context = context;
        _logger = logger;
    }
}