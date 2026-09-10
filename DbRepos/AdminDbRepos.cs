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

    public async Task SeedAsync(int nrItems)
    {
        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);

        _logger.LogInformation($"WD: {Directory.GetCurrentDirectory()}");
        _logger.LogInformation($"Seed file path: {fn}");
        _logger.LogInformation($"Seed file exists: {File.Exists(fn)}");

        var seeder = new SeedGenerator(fn);

        // //remove existing attractions in the database
        // _dbContext.Attractions.RemoveRange(_dbContext.Attractions);

        //Seeding new attractions into the database
        var attractions = seeder.ItemsToList<AttractionDbM>(nrItems);
        _dbContext.Attractions.AddRange(attractions);

        var categories = seeder.ItemsToList<CategoryDbM>(nrItems);
        _dbContext.Categories.AddRange(categories);

        var addresses = seeder.ItemsToList<AddressDbM>(nrItems);
        _dbContext.Addresses.AddRange(addresses);

        var users = seeder.ItemsToList<UserDbM>(nrItems);
        _dbContext.Users.AddRange(users);

        var reviews = seeder.ItemsToList<ReviewDbM>(nrItems);
        _dbContext.Reviews.AddRange(reviews);

        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
