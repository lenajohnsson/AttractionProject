using DbRepos;
using Microsoft.Extensions.Logging;
using Models.DTO;

namespace Services;

public class UsersService : IUsersService
{
    readonly UsersDbRepo _repo;
    ILogger<UsersService> _logger;

    public UsersService(UsersDbRepo repo,
                        ILogger<UsersService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public Task<ResponsePageDto<UserReadDto>> ReadUsersAsync(int pageNr, int pageSz) =>
        _repo.ReadUsersAsync(pageNr, pageSz);
}