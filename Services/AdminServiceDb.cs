using DbRepos;
using Models.DTO;

namespace Services;

public class AdminServiceDb : IAdminService
{
    private readonly AdminDbRepos _repo = null;

    public AdminServiceDb(AdminDbRepos repo)
    {
        _repo = repo;
    }

    public Task<ResponseItemDto<GuestUserInfoAllDto>> SeedAsync(int nrItems) => _repo.SeedAsync(nrItems);
    public Task<ResponseItemDto<GuestUserInfoAllDto>> RemoveSeedAsync(bool seeded) => _repo.RemoveSeedAsync(seeded);
    public Task<ResponseItemDto<GuestUserInfoAllDto>> OverviewAsync() => _repo.OverviewAsync();
}

