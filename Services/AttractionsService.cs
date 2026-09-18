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
    public async Task<ResponsePageDto<AttractionNoCommentDto>> ReadAttractionsWithoutReviewAsync(int pageNr, int pageSz)
    {
        var result = await _repo.ReadAttractionsWithoutReviewAsync(pageNr, pageSz);

        var dtoItems = result.PageItems.Select(a => new AttractionNoCommentDto
        {
            AttractionId = a.AttractionId,
            AttractionName = a.AttractionName,
            Description = a.Description,
            Country = a.Address?.Country,
            Reviews = a.Reviews?.Select(r => r.Comment).ToList()
        }).ToList();

        return new ResponsePageDto<AttractionNoCommentDto>
        {
            DbItemsCount = result.DbItemsCount,
            PageItems = dtoItems,
            PageNr = result.PageNr,
            PageSize = result.PageSize
        };

    }

    public async Task<ResponseItemDto<AttractionReadItemDto>> ReadAttractionAsync(Guid id, bool flat)
    {
        var result = await _repo.ReadAttractionAsync(id, flat);
        var attr = result.Item;

        var dtoItems = new AttractionReadItemDto
        {
            AttractionId = attr.AttractionId,
            AttractionName = attr.AttractionName,
            Description = attr.Description,
            Categories = attr.Categories?.Select(c => c.CategoryType).ToList(),
            Reviews = attr.Reviews?.Select(r => new ReviewReadDto
            {
                Comment = r.Comment,
                ReviewGrade = r.ReviewGrade,
                Date = r.Date
            }).ToList()
        };

        return new ResponseItemDto<AttractionReadItemDto>
        {
            Item = dtoItems
        };
    }
}