using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;

public class AttractionsService : IAttractionsService
{
    readonly AttractionsDbRepo _repo;
    ILogger<AttractionsService> _logger;

    public AttractionsService(AttractionsDbRepo repo,
                            ILogger<AttractionsService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<ResponsePageDto<AttractionReadListDto>> ReadAttractionsAsync(bool seeded, bool flat, string filter, int pageNr, int pageSz)
    {
        var result = await _repo.ReadAttractionsAsync(seeded, flat, filter, pageNr, pageSz);

        var dtoItems = result.PageItems.Select(a => new AttractionReadListDto
        {
            AttractionId = a.AttractionId,
            AttractionName = a.AttractionName,
            Description = a.Description,
            City = a.Address?.City,
            Country = a.Address?.Country,
            Categories = a.Categories?.Select(c => c.CategoryType).ToList()
        }).ToList();

        return new ResponsePageDto<AttractionReadListDto>
        {
            DbItemsCount = result.DbItemsCount,
            PageItems = dtoItems,
            PageNr = result.PageNr,
            PageSize = result.PageSize
        };
    }
    public Task<ResponsePageDto<IAttraction>> ReadAttractionsWithoutReviewAsync(int pageNr, int pageSz) => _repo.ReadAttractionsWithoutReviewAsync(pageNr, pageSz);

    public Task<ResponseItemDto<IAttraction>> ReadAttractionAsync(Guid id, bool flat) => _repo.ReadAttractionAsync(id, flat);
}