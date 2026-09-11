using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Models.DTO;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;
    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }

    public async Task<ResponseItemDto<GuestUserInfoAllDto>> InfoAsync() => await DbInfo();

    private async Task<ResponseItemDto<GuestUserInfoAllDto>> DbInfo()
    {
        var info = new GuestUserInfoAllDto();
        info.Db = new GuestUserInfoDto
        {
            NrSeededAttractions = await _dbContext.Attractions.Where(a => a.Seeded).CountAsync(),
            NrUnSeededAttractions = await _dbContext.Attractions.Where(a => !a.Seeded).CountAsync(),
            NrAttractionsWithReviews = await _dbContext.Attractions.Where(a => a.ReviewsDbM != null).CountAsync(),

            NrSeededAddresses = await _dbContext.Addresses.Where(a => a.Seeded).CountAsync(),
            NrUnseededAddresses = await _dbContext.Addresses.Where(a => !a.Seeded).CountAsync(),

            NrSeededUsers = await _dbContext.Users.Where(u => u.Seeded).CountAsync(),
            NrUnseededUsers = await _dbContext.Users.Where(u => !u.Seeded).CountAsync(),

            NrSeededReviews = await _dbContext.Reviews.Where(r => r.Seeded).CountAsync(),
            NrUnseededReviews = await _dbContext.Reviews.Where(r => !r.Seeded).CountAsync(),

            NrSeededCategories = await _dbContext.Categories.Where(c => c.Seeded).CountAsync(),
            NrUnseededCategories = await _dbContext.Categories.Where(c => !c.Seeded).CountAsync(),
        };

        return new ResponseItemDto<GuestUserInfoAllDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = info
        };
    }


    public async Task<ResponseItemDto<GuestUserInfoAllDto>> SeedAsync(int nrItems)
    {
        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        // minst 50 användare, 
        // 11 länder, 
        // 100 städer, 
        // 1000 sevärdheter
        // 0 - 20 kommentarer

        var attractions = seeder.UniqueItemsToList<AttractionDbM>(nrItems);
        var attractionAddresses = seeder.UniqueItemsToList<AddressDbM>(nrItems);
        var userAddresses = seeder.UniqueItemsToList<AddressDbM>(nrItems);
        var users = seeder.ItemsToList<UserDbM>(nrItems);

        foreach (var user in users)
        {

            user.AddressDbM = seeder.FromList(seeder.UniqueItemsPickedFromList(1, userAddresses));
        }

        foreach (var attraction in attractions)
        {
            attraction.AddressDbM = seeder.FromList(attractionAddresses);
            attraction.CategoriesDbM = seeder.ItemsToList<CategoryDbM>(seeder.Next(1, 3));
            attraction.ReviewsDbM = seeder.ItemsToList<ReviewDbM>(seeder.Next(1, 21));
            attraction.UsersDbM = seeder.ItemsToList<UserDbM>(seeder.Next(1, 6));
        }

        _dbContext.Attractions.AddRange(attractions);

        //Save changes to the database
        await _dbContext.SaveChangesAsync();

        return await DbInfo();
    }

    public async Task<ResponseItemDto<GuestUserInfoAllDto>> RemoveSeedAsync(bool seeded)
    {
        _dbContext.Users.RemoveRange(_dbContext.Users.Where(u => u.Seeded == seeded));
        _dbContext.Addresses.RemoveRange(_dbContext.Addresses.Where(u => u.Seeded == seeded));
        _dbContext.Attractions.RemoveRange(_dbContext.Attractions.Where(u => u.Seeded == seeded));
        _dbContext.Reviews.RemoveRange(_dbContext.Reviews.Where(u => u.Seeded == seeded));
        _dbContext.Categories.RemoveRange(_dbContext.Categories.Where(u => u.Seeded == seeded));

        await _dbContext.SaveChangesAsync();

        return await DbInfo();
    }

}
