using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoriesController : Controller
    {
        readonly ICategoriesService _service;
        ILogger<CategoriesController> _logger;

        //GET: api/categories/read
        [HttpGet()]
        [ActionName("Read")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<ICategory>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read()
        {
            try
            {
                _logger.LogInformation($"{nameof(Read)}");
                var res = await _service.ReadCategoriesAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Read)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        public CategoriesController(ICategoriesService service,
                                    ILogger<CategoriesController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}