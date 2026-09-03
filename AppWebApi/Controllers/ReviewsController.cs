using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReviewsController : Controller
    {
        readonly IReviewsService _service;
        ILogger<ReviewsController> _logger;

        public ReviewsController(IReviewsService service,
                                    ILogger<ReviewsController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}