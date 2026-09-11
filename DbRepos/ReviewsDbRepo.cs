
using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace DbRepos;

public class ReviewsDbRepo
{
    readonly MainDbContext _dbContext;
    ILogger<ReviewsDbRepo> _logger;

    public ReviewsDbRepo(MainDbContext context,
                            ILogger<ReviewsDbRepo> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    public async Task<ResponsePageDto<IReview>> ReadReviewsAsync()
    {
        IQueryable<ReviewDbM> query = _dbContext.Reviews;
        var ret = new ResponsePageDto<IReview>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query.CountAsync(),
            PageItems = await query.ToListAsync<IReview>()
        };
        return ret;
    }
}