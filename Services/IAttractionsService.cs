using Models;
using Models.DTO;

namespace Services;

public interface IAttractionsService
{
    public Task<ResponsePageDto<AttractionReadListDto>> ReadAttractionsAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz);

    public Task<ResponsePageDto<IAttraction>> ReadAttractionsWithoutReviewAsync(int pageNr, int pageSz);

    public Task<ResponseItemDto<IAttraction>> ReadAttractionAsync(Guid id, bool flat);
}