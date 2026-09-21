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

    public Task<ResponsePageDto<IReview>> ReadReviewsAsync() => _repo.ReadReviewsAsync();
}