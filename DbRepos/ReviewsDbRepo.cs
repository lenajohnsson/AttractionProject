using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace DbRepos;

public class ReviewsDbRepo
{
    readonly MainDbContext _dbContext;

    public ReviewsDbRepo(MainDbContext context)
    {
        _dbContext = context;
    }

    public async Task<ResponseItemDto<IReview>> ReadReviewAsync(Guid id, bool flat)
    {
        IReview item;
        if (flat)
        {
            var query = _dbContext.Reviews
                .Where(i => i.ReviewId == id);

            item = await query.FirstOrDefaultAsync<IReview>();
        }
        else
        {
            var query = _dbContext.Reviews
                .Include(i => i.AttractionDbM)
                .Include(i => i.UserDbM)
                .Where(i => i.ReviewId == id);

            item = await query.FirstOrDefaultAsync<IReview>();
        }

        if (item == null)
            throw new ArgumentException($"Review {id} does not exist");

        return new ResponseItemDto<IReview>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }

    public async Task<ResponseItemDto<IReview>> CreateReviewAsync(ReviewCuDto itemDto)
    {
        if (itemDto.ReviewId != null)
            throw new ArgumentException($"{nameof(itemDto.ReviewId)} must be null when creating a new review");

        var item = new ReviewDbM(itemDto);

        await UpdateNavProp(itemDto, item);

        _dbContext.Reviews.Add(item);

        await _dbContext.SaveChangesAsync();

        return await ReadReviewAsync(item.ReviewId, false);
    }

    public async Task<ResponseItemDto<IReview>> DeleteReviewAsync(Guid id)
    {
        var query = _dbContext.Reviews
            .Where(i => i.ReviewId == id);

        var item = await query.FirstOrDefaultAsync<ReviewDbM>();

        if (item == null)
            throw new ArgumentException($"Review {id} does not exist");

        _dbContext.Reviews.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IReview>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }

    private async Task UpdateNavProp(ReviewCuDto itemSrc, ReviewDbM itemDst)
    {
        itemDst.AttractionDbM = (itemSrc.AttractionId != null) ?
            await _dbContext.Attractions.FirstOrDefaultAsync(a => a.AttractionId == itemSrc.AttractionId) : null;

        itemDst.UserDbM = (itemSrc.UserId != null) ?
            await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == itemSrc.UserId) : null;
    }
}