using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

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

    public Task<ResponsePageDto<IAddress>> ReadAddressesAsync(bool seeded, bool flat, string filter, int pageNr, int pageSz) => _repo.ReadAddressesAsync(seeded, flat, filter, pageNr, pageSz);
}