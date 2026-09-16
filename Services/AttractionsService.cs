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

    public async Task<ResponsePageDto<AttractionReadListDto>> ReadAttractionsAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz)
    {
        var efResult = await _repo.ReadAttractionsAsync(seeded, flat, filter, pageNr, pageSz);

        var dtoItems = efResult.PageItems.Select(a => new AttractionReadListDto
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
            DbItemsCount = efResult.DbItemsCount,
            PageItems = dtoItems,
            PageNr = efResult.PageNr,
            PageSize = efResult.PageSize
        };
    }
}