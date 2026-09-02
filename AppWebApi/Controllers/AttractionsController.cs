using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AttractionsController : Controller
    {
        readonly IAttractionsService _service;
        ILogger<AttractionsController> _logger;

        public AttractionsController(IAttractionsService service,
                                    ILogger<AttractionsController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}