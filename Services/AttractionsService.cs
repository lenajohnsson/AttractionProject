using DbRepos;
using Microsoft.Extensions.Logging;

namespace Services;

public class AttractionsService : IAttractionsService
{
    readonly AttractionsDbRepo _repo;
    ILogger<AttractionsService> _logger;

    public AttractionsService(AttractionsDbRepo repo,
                                ILogger<AttractionsService> logger)
    {
        _repo = repo;
        _logger = logger;
    }
}