using DbRepos;
using Models;
using Models.DTO;

namespace Services;

public class CategoriesService : ICategoriesService
{
    readonly CategoriesDbRepo _repo;

    public CategoriesService(CategoriesDbRepo repo)
    {
        _repo = repo;
    }

    public Task<ResponsePageDto<ICategory>> ReadCategoriesAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz) =>
        _repo.ReadCategoriesAsync(seeded, flat, filter, pageNr, pageSz);
}