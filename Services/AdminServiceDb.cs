using Microsoft.Extensions.Logging;

using DbRepos;
using Models.DTO;

namespace Services;

public class AdminServiceDb : IAdminService
{
    private readonly AdminDbRepos _repo = null;
    private readonly ILogger<AdminServiceDb> _logger = null;

    public AdminServiceDb(AdminDbRepos repo)
    {
        _repo = repo;
    }
    public AdminServiceDb(AdminDbRepos repo, ILogger<AdminServiceDb> logger) : this(repo)
    {
        _logger = logger;
    }

    public Task<ResponseItemDto<GuestUserInfoAllDto>> SeedAsync(int nrItems) => _repo.SeedAsync(nrItems);
    public Task<ResponseItemDto<GuestUserInfoAllDto>> RemoveSeedAsync(bool seeded) => _repo.RemoveSeedAsync(seeded);
}

