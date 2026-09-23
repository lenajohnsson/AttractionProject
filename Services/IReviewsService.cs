using Models;
using Models.DTO;

namespace Services;

public interface IReviewsService
{
    public Task<ResponseItemDto<IReview>> ReadReviewAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IReview>> CreateReviewAsync(ReviewCuDto itemDto);
}