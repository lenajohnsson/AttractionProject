using DbRepos;
using Microsoft.Extensions.Logging;

namespace Services;

public class AddressesService : IAddressesService
{
    readonly AddressesDbRepo _repo;
    ILogger<AddressesService> _logger;

    public AddressesService(AddressesDbRepo repo,
                            ILogger<AddressesService> logger)
    {
        _repo = repo;
        _logger = logger;
    }
}