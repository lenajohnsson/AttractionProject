using Microsoft.EntityFrameworkCore;
using System.Data;
using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Models.DTO;
using Microsoft.Data.SqlClient;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly MainDbContext _dbContext;
    public AdminDbRepos(MainDbContext context)
    {
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
            NrAttractionsWithReviews = await _dbContext.Attractions.Where(a => a.ReviewsDbM.Any()).CountAsync(),

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

    public async Task<ResponseItemDto<GuestUserInfoAllDto>> OverviewAsync()
    {
        var info = new GuestUserInfoAllDto
        {
            Overview = await _dbContext.InfoView.FirstAsync()
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
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        // minst 50 användare, 
        // 11 länder, 
        // 110 städer, 
        // 1000 sevärdheter
        // 0 - 20 kommentarer

        var attractions = seeder.UniqueItemsToList<AttractionDbM>(nrItems);
        var attractionAddresses = seeder.UniqueItemsToList<AddressDbM>(nrItems);
        var users = seeder.ItemsToList<UserDbM>(50);

        foreach (var attraction in attractions)
        {
            attraction.AddressDbM = seeder.FromList(attractionAddresses);
            attraction.CategoriesDbM = seeder.ItemsToList<CategoryDbM>(seeder.Next(1, 3));
            var reviews = seeder.ItemsToList<ReviewDbM>(seeder.Next(0, 21));

            foreach (var review in reviews)
            {
                review.UserDbM = seeder.FromList(users);
            }

            attraction.ReviewsDbM = seeder.Bool ? reviews : null;
        }

        _dbContext.Users.AddRange(users);
        _dbContext.Attractions.AddRange(attractions);

        await _dbContext.SaveChangesAsync();

        return await DbInfo();
    }

    public async Task<ResponseItemDto<GuestUserInfoAllDto>> RemoveSeedAsync(bool seeded)
    {
        var connection = _dbContext.Database.GetDbConnection();
        using var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandText = "dbo.spDeleteAll";

        cmd.Parameters.Add(new SqlParameter("@seededParam", seeded));

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        await cmd.ExecuteScalarAsync();

        return await DbInfo();
    }
}
