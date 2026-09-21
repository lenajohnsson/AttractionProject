using Models.DTO;

namespace Services;

public interface IUsersService
{
    public Task<ResponsePageDto<UserReadDto>> ReadUsersAsync(int pageNr, int pageSz);
}