using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace DbRepos;

public class AttractionsDbRepo
{
    readonly MainDbContext _dbContext;

    public AttractionsDbRepo(MainDbContext context)
    {
        _dbContext = context;
    }

    public async Task<ResponsePageDto<AttractionReadListDto>> ReadAttractionsAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz)
    {
        if (filter == null)
            filter = "";

        IQueryable<AttractionDbM> query;

        if (flat)
        {
            query = _dbContext.Attractions;
        }
        else
        {
            query = _dbContext.Attractions
                .Include(i => i.AddressDbM)
                .Include(i => i.CategoriesDbM);
        }

        var pageItems = await query
            .Where(i => (i.Seeded == seeded) &&
                        (i.AttractionName.ToLower().Contains(filter) ||
                        i.Description.ToLower().Contains(filter) ||
                        i.CategoriesDbM.Any(c => c.CategoryType.ToLower().Contains(filter)) ||
                        i.AddressDbM.Country.ToLower().Contains(filter) ||
                        i.AddressDbM.City.ToLower().Contains(filter)))
            .Skip(pageNr * pageSz)
            .Take(pageSz)
            .ToListAsync();

        var dtoItems = pageItems.Select(a => new AttractionReadListDto
        {
            AttractionId = a.AttractionId,
            AttractionName = a.AttractionName,
            Description = a.Description,
            City = a.Address?.City,
            Country = a.Address?.Country,
            Categories = a.Categories?.Select(c => c.CategoryType).ToList()
        }).ToList();

        return new ResponsePageDto<AttractionReadListDto>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
            .Where(i => (i.Seeded == seeded) &&
                        (i.AttractionName.ToLower().Contains(filter) ||
                        i.Description.ToLower().Contains(filter) ||
                        i.CategoriesDbM.Any(c => c.CategoryType.ToLower().Contains(filter)) ||
                        i.AddressDbM.Country.ToLower().Contains(filter) ||
                        i.AddressDbM.City.ToLower().Contains(filter))).CountAsync(),

            PageItems = dtoItems,

            PageNr = pageNr,
            PageSize = pageSz
        };
    }

    public async Task<ResponsePageDto<AttractionNoCommentDto>> ReadAttractionsWithoutReviewAsync(int pageNr, int pageSz)
    {
        IQueryable<AttractionDbM> query = _dbContext.Attractions
            .Include(i => i.ReviewsDbM)
            .Include(i => i.AddressDbM);

        var pageItems = await query
                .Where(i => !i.ReviewsDbM.Any())
                .Skip(pageNr * pageSz)
                .Take(pageSz)
                .ToListAsync();

        var dtoItems = pageItems.Select(a => new AttractionNoCommentDto
        {
            AttractionId = a.AttractionId,
            AttractionName = a.AttractionName,
            Description = a.Description,
            Country = a.Address?.Country,
            Reviews = a.Reviews?.Select(r => r.Comment).ToList()
        }).ToList();

        return new ResponsePageDto<AttractionNoCommentDto>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query
                .Where(i => !i.ReviewsDbM.Any()).CountAsync(),

            PageItems = dtoItems,

            PageNr = pageNr,
            PageSize = pageSz
        };
    }

    public async Task<ResponseItemDto<AttractionReadItemDto>> ReadAttractionAsync(Guid id, bool flat)
    {
        IAttraction item;
        if (flat)
        {
            var query = _dbContext.Attractions
                .Where(i => i.AttractionId == id);

            item = await query.FirstOrDefaultAsync<IAttraction>();
        }
        else
        {
            var query = _dbContext.Attractions
                .Include(i => i.CategoriesDbM)
                .Include(i => i.ReviewsDbM)
                .Where(i => i.AttractionId == id);

            item = await query.FirstOrDefaultAsync<IAttraction>();
        }

        if (item == null) throw new ArgumentException($"Attraction {id} does not exist");

        var dtoItems = new AttractionReadItemDto
        {
            AttractionId = item.AttractionId,
            AttractionName = item.AttractionName,
            Description = item.Description,
            Categories = item.Categories?.Select(c => c.CategoryType).ToList(),
            Reviews = item.Reviews?.Select(r => new ReviewReadDto
            {
                ReviewId = r.ReviewId,
                Comment = r.Comment,
                ReviewGrade = r.ReviewGrade,
                Date = r.Date
            }).ToList()
        };

        return new ResponseItemDto<AttractionReadItemDto>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = dtoItems
        };
    }

    public async Task<ResponseItemDto<AttractionReadItemDto>> CreateAttractionAsync(AttractionCuDto itemDto)
    {
        if (itemDto.AttractionId != null)
            throw new ArgumentException($"{nameof(itemDto.AttractionId)} must be null when creating a new attraction");

        var item = new AttractionDbM(itemDto);

        _dbContext.Attractions.Add(item);

        await _dbContext.SaveChangesAsync();

        return await ReadAttractionAsync(item.AttractionId, false);
    }

    public async Task<ResponseItemDto<AttractionReadItemDto>> UpdateAttractionAsync(AttractionCuDto itemDto)
    {
        var query = _dbContext.Attractions
            .Where(i => i.AttractionId == itemDto.AttractionId);

        var item = await query
            .Include(i => i.AddressDbM)
            .Include(i => i.ReviewsDbM)
            .Include(i => i.CategoriesDbM)
            .FirstOrDefaultAsync<AttractionDbM>();

        if (item == null)
            throw new ArgumentException($"Attraction {itemDto.AttractionId} does not exist");

        item.UpdateFromDto(itemDto);

        await UpdateNavProp(itemDto, item);

        _dbContext.Attractions.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadAttractionAsync(item.AttractionId, false);
    }

    public async Task<ResponseItemDto<IAttraction>> DeleteAttractionAsync(Guid id)
    {
        var query = _dbContext.Attractions
            .Where(i => i.AttractionId == id);

        var item = await query.FirstOrDefaultAsync<AttractionDbM>();

        if (item == null)
            throw new ArgumentException($"Attraction {id} does not exist");

        _dbContext.Attractions.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IAttraction>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }

    private async Task UpdateNavProp(AttractionCuDto itemSrc, AttractionDbM itemDst)
    {
        itemDst.AddressDbM = (itemSrc.AddressId != null) ?
            await _dbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == itemSrc.AddressId) : null;

        List<ReviewDbM> reviews = null;
        if (itemSrc.ReviewId != null)
        {
            reviews = new List<ReviewDbM>();
            foreach (var id in itemSrc.ReviewId)
            {
                var rev = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);
                if (rev == null)
                    throw new ArgumentException($"Review {id} does not exist");

                reviews.Add(rev);
            }
        }
        itemDst.ReviewsDbM = reviews;

        List<CategoryDbM> categories = null;
        if (itemSrc.CategoryId != null)
        {
            categories = new List<CategoryDbM>();
            foreach (var id in itemSrc.CategoryId)
            {
                var cat = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (cat == null)
                    throw new ArgumentException($"Category {id} does not exist");

                categories.Add(cat);
            }
        }
        itemDst.CategoriesDbM = categories;
    }
}