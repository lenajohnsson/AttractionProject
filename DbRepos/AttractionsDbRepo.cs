
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

    // Task - någon gång i framtiden får du ett värde av typen T
    // ResponsePageDto<IAttraction> är typen som returneras
    public async Task<ResponsePageDto<IAttraction>> ReadAttractionsAsync()
    {
        // hämta alla AttractionDbM från databasen
        IQueryable<AttractionDbM> query = _dbContext.Attractions;
        // Skapar ett nytt objekt av typen ResponsePageDto<IAttraction>
        var ret = new ResponsePageDto<IAttraction>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            // SELECT COUNT(*) FROM Attractions
            // antal rader i tabellen
            DbItemsCount = await query.CountAsync(),
            // SELECT * FROM Attractions
            // alla attractions som en lista
            PageItems = await query.ToListAsync<IAttraction>()
        };
        return ret;
    }
}