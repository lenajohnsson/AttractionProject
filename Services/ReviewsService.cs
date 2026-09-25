using DbRepos;
using Models;
using Models.DTO;

namespace Services;

public class ReviewsService : IReviewsService
{
    readonly ReviewsDbRepo _repo;

    public ReviewsService(ReviewsDbRepo repo)
    {
        _repo = repo;
    }

    public Task<ResponseItemDto<IReview>> ReadReviewAsync(Guid id, bool flat) =>
        _repo.ReadReviewAsync(id, flat);

    public Task<ResponseItemDto<IReview>> CreateReviewAsync(ReviewCuDto itemDto) =>
        _repo.CreateReviewAsync(itemDto);

    public Task<ResponseItemDto<IReview>> DeleteReviewAsync(Guid id) =>
        _repo.DeleteReviewAsync(id);
}