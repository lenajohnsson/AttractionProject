using DbRepos;
using Microsoft.Extensions.Logging;
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

    public Task<ResponsePageDto<AttractionReadListDto>> ReadAttractionsAsync(bool seeded, bool flat, string filter, int pageNr, int pageSz) =>
        _repo.ReadAttractionsAsync(seeded, flat, filter, pageNr, pageSz);

    public Task<ResponsePageDto<AttractionNoCommentDto>> ReadAttractionsWithoutReviewAsync(int pageNr, int pageSz) =>
        _repo.ReadAttractionsWithoutReviewAsync(pageNr, pageSz);

    public Task<ResponseItemDto<AttractionReadItemDto>> ReadAttractionAsync(Guid id, bool flat) =>
        _repo.ReadAttractionAsync(id, flat);

}