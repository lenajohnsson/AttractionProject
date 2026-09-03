using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoriesController : Controller
    {
        readonly ICategoriesService _service;
        ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoriesService service,
                                    ILogger<CategoriesController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}