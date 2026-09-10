using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;

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


    public async Task SeedAsync(int nrItems)
    {
        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        // 50 användare, 100 städer, 4 länder, 1000 sevärdheter
        // 0 - 20 kommentarer

        //Seeding new attractions into the database
        var attractions = seeder.ItemsToList<AttractionDbM>(nrItems);
        var addresses = seeder.ItemsToList<AddressDbM>(nrItems);


        foreach (var attraction in attractions)
        {
            attraction.AddressDbM = seeder.FromList(addresses);
            attraction.CategoriesDbM = seeder.ItemsToList<CategoryDbM>(seeder.Next(1, 3));
            attraction.UsersDbM = seeder.ItemsToList<UserDbM>(seeder.Next(1, 11));
            attraction.ReviewsDbM = seeder.ItemsToList<ReviewDbM>(seeder.Next(0, 21));
        }

        _dbContext.Attractions.AddRange(attractions);

        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

}
