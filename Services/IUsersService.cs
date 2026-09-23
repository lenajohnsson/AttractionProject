using Models;
using Models.DTO;

namespace Services;

public interface IUsersService
{
    public Task<ResponsePageDto<UserReadDto>> ReadUsersAsync(int pageNr, int pageSz);
    public Task<ResponseItemDto<IUser>> ReadUserAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IUser>> CreateUserAsync(UserCuDto item);
    public Task<ResponseItemDto<IUser>> DeleteUserAsync(Guid id);
}