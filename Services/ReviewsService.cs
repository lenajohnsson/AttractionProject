using DbRepos;
using Microsoft.Extensions.Logging;

namespace Services;

public class ReviewsService : IReviewsService
{
    readonly ReviewsDbRepo _repo;
    ILogger<ReviewsService> _logger;

    public ReviewsService(ReviewsDbRepo repo,
                                ILogger<ReviewsService> logger)
    {
        _repo = repo;
        _logger = logger;
    }
}