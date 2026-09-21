using DbRepos;
using Models;
using Models.DTO;

namespace Services;

public class AddressesService : IAddressesService
{
    readonly AddressesDbRepo _repo;

    public AddressesService(AddressesDbRepo repo)
    {
        _repo = repo;
    }

    public Task<ResponsePageDto<IAddress>> ReadAddressesAsync
        (bool seeded, bool flat, string filter, int pageNr, int pageSz) =>
        _repo.ReadAddressesAsync(seeded, flat, filter, pageNr, pageSz);
}