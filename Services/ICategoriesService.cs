using Models;
using Models.DTO;

namespace Services;

public interface ICategoriesService
{
    public Task<ResponsePageDto<ICategory>> ReadCategoriesAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz);
}