using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
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

    public async Task<ResponsePageDto<UserReadDto>> ReadUsersAsync(int pageNr, int pageSz)
    {
        var result = await _repo.ReadUsersAsync(pageNr, pageSz);

        var dtoItems = result.PageItems.Select(u => new UserReadDto
        {
            UserId = u.UserId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Reviews = u.Reviews?.Select(r => new ReviewReadDto
            {
                Comment = r.Comment,
                ReviewGrade = r.ReviewGrade,
                Date = r.Date
            }).ToList()
        }).ToList();

        return new ResponsePageDto<UserReadDto>
        {
            DbItemsCount = result.DbItemsCount,
            PageItems = dtoItems,
            PageNr = result.PageNr,
            PageSize = result.PageSize
        };
    }
}