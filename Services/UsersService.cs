using DbRepos;
using Models.DTO;

namespace Services;

public class UsersService : IUsersService
{
    readonly UsersDbRepo _repo;

    public UsersService(UsersDbRepo repo)
    {
        _repo = repo;
    }

    public Task<ResponsePageDto<UserReadDto>> ReadUsersAsync(int pageNr, int pageSz) =>
        _repo.ReadUsersAsync(pageNr, pageSz);
}