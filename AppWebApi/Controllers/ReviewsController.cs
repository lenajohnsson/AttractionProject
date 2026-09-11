using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReviewsController : Controller
    {
        readonly IReviewsService _service;
        ILogger<ReviewsController> _logger;

        //GET: api/reviews/read
        [HttpGet()]
        [ActionName("Read")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IReview>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read()
        {
            try
            {
                _logger.LogInformation($"{nameof(Read)}");
                var res = await _service.ReadReviewsAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Read)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        public ReviewsController(IReviewsService service,
                                    ILogger<ReviewsController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}