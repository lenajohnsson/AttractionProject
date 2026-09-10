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

        // minst 50 användare, 
        // 11 länder, 
        // 100 städer, 
        // 1000 sevärdheter
        // 0 - 20 kommentarer


        var attractions = seeder.ItemsToList<AttractionDbM>(nrItems);
        var attractionAddresses = seeder.UniqueItemsToList<AddressDbM>(nrItems);
        var userAddresses = seeder.UniqueItemsToList<AddressDbM>(nrItems);
        var users = seeder.ItemsToList<UserDbM>(100);
        // var reviews = seeder.ItemsToList<ReviewDbM>(nrItems);

        foreach (var user in users)
        {
            user.AddressDbM = seeder.FromList(userAddresses);

        }

        foreach (var attraction in attractions)
        {
            attraction.AddressDbM = seeder.FromList(attractionAddresses);
            attraction.CategoriesDbM = seeder.ItemsToList<CategoryDbM>(seeder.Next(1, 3));
            attraction.UsersDbM = seeder.ItemsToList<UserDbM>(seeder.Next(1, 6));

        }

        _dbContext.Users.AddRange(users);
        _dbContext.Attractions.AddRange(attractions);

        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

}
