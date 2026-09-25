using Models;
using Models.DTO;

namespace Services;

public interface IAttractionsService
{
    public Task<ResponsePageDto<AttractionReadListDto>> ReadAttractionsAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz);
    public Task<ResponsePageDto<AttractionNoCommentDto>> ReadAttractionsWithoutReviewAsync(int pageNr, int pageSz);
    public Task<ResponseItemDto<AttractionReadItemDto>> ReadAttractionAsync(Guid id, bool flat);
    public Task<ResponseItemDto<AttractionReadItemDto>> CreateAttractionAsync(AttractionCuDto itemDto);
    public Task<ResponseItemDto<IAttraction>> DeleteAttractionAsync(Guid id);
}