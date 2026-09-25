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

        public ReviewsController(IReviewsService service,
                                    ILogger<ReviewsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        //GET: api/reviews/readreview
        [HttpGet()]
        [ActionName("ReadReview")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IReview>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadReview(string id = null, string flat = "false")
        {
            try
            {
                Guid idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadReview)}: {nameof(idArg)} - {idArg}, {nameof(flatArg)} - {flatArg}");
                var item = await _service.ReadReviewAsync(idArg, flatArg);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadReview)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //POST: api/users/createReview
        [HttpPost()]
        [ActionName("CreateReview")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IReview>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCuDto item)
        {
            try
            {
                item.EnsureValidity();

                _logger.LogInformation($"{nameof(CreateReview)}");

                var _item = await _service.CreateReviewAsync(item);

                _logger.LogInformation($"Item {_item.Item.ReviewId} created");

                return Ok(_item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateReview)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"Could not create item. Error {ex.Message} - {ex.InnerException}");
            }
        }

        //DELETE: api/users/deletereview
        [HttpDelete("{id}")]
        [ActionName("DeleteReview")]
        [ProducesResponseType(200, Type = typeof(IReview))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteReview(string id)
        {
            try
            {
                Guid idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(DeleteReview)}: {nameof(idArg)} - {nameof(idArg)}");

                var item = await _service.DeleteReviewAsync(idArg);
                if (item == null)
                    throw new ArgumentException($"Review with id {id} does not exist");

                _logger.LogInformation($"Review {idArg} is deleted");

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteReview)}: {ex.Message} - {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }
    }
}