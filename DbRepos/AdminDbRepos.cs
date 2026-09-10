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
        var seeder = new SeedGenerator(fn);



        //Seeding new attractions into the database
        var attractions = seeder.ItemsToList<AttractionDbM>(nrItems);
        var categories = seeder.ItemsToList<CategoryDbM>(nrItems);
        var addresses = seeder.ItemsToList<AddressDbM>(nrItems);
        var users = seeder.ItemsToList<UserDbM>(nrItems);
        var reviews = seeder.ItemsToList<ReviewDbM>(nrItems);

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
