using Models;
using Models.DTO;

namespace Services;

public interface ICategoriesService
{
    public Task<ResponsePageDto<ICategory>> ReadCategoriesAsync();
}