using Models.DTO;

namespace Services;

public interface IAdminService
{
    public Task<ResponseItemDto<GuestUserInfoAllDto>> SeedAsync(int nrItems);
    public Task<ResponseItemDto<GuestUserInfoAllDto>> RemoveSeedAsync(bool seeded);
    public Task<ResponseItemDto<GuestUserInfoAllDto>> OverviewAsync();
}
