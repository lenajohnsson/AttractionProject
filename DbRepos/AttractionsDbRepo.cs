
using DbContext;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace DbRepos;

public class AttractionsDbRepo
{
    readonly MainDbContext _dbContext;
    ILogger<AttractionsDbRepo> _logger;

    public AttractionsDbRepo(MainDbContext context,
                            ILogger<AttractionsDbRepo> logger)
    {
        _dbContext = context;
        _logger = logger;
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

        var ret = new ResponsePageDto<AttractionReadListDto>()
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
        return ret;
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

        var ret = new ResponsePageDto<AttractionNoCommentDto>()
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
        return ret;
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

        if (item == null) throw new ArgumentException($"Item {id} does not exist");

        var dtoItems = new AttractionReadItemDto
        {
            AttractionId = item.AttractionId,
            AttractionName = item.AttractionName,
            Description = item.Description,
            Categories = item.Categories?.Select(c => c.CategoryType).ToList(),
            Reviews = item.Reviews?.Select(r => new ReviewReadDto
            {
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


}