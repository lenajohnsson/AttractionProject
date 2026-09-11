using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

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

    public Task<ResponsePageDto<ICategory>> ReadCategoriesAsync() => _repo.ReadCategoriesAsync();
}