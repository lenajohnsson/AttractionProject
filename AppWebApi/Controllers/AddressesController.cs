using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AddressesController : Controller
    {
        readonly IAddressesService _service;
        ILogger<AddressesController> _logger;

        public AddressesController(IAddressesService service,
                                    ILogger<AddressesController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}