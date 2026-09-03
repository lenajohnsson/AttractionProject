using DbRepos;
using Microsoft.Extensions.Logging;

namespace Services;

public class CategoriesService : ICategoriesService
{
    readonly CategoriesDbRepo _repo;
    ILogger<CategoriesService> _logger;

    public CategoriesService(CategoriesDbRepo repo,
                                ILogger<CategoriesService> logger)
    {
        _repo = repo;
        _logger = logger;
    }
}