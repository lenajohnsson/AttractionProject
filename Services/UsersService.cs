using DbRepos;
using Models;
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
    public Task<ResponseItemDto<IUser>> ReadUserAsync(Guid id, bool flat) =>
        _repo.ReadUserAsync(id, flat);
    public Task<ResponseItemDto<IUser>> CreateUserAsync(UserCuDto item) =>
        _repo.CreateUserAsync(item);
}