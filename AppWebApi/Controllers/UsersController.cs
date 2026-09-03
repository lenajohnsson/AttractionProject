using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        readonly IUsersService _service;
        ILogger<UsersController> _logger;

        public UsersController(IUsersService service,
                                    ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}