using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AttractionsController : Controller
    {
        readonly IAttractionsService _service;
        ILogger<AttractionsController> _logger;

        //GET: api/attractions/read
        [HttpGet()]
        [ActionName("Read")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read
            (string seeded = "true", string flat = "true", string filter = null,
            string pageNr = "0", string pageSz = "10")
        {
            try
            {
                bool seededArg = bool.Parse(seeded);
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSzArg = int.Parse(pageSz);

                _logger.LogInformation($"{nameof(Read)}: {nameof(seededArg)} - {seededArg}, " +
                    $"{nameof(flatArg)} - {flatArg}, {nameof(pageNrArg)} - {pageNrArg}, " +
                    $"{nameof(pageSzArg)} - {pageSzArg} ");

                var res = await _service.ReadAttractionsAsync(seededArg, flatArg, filter?.Trim().ToLower(),
                    pageNrArg, pageSzArg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Read)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        public AttractionsController(IAttractionsService service,
                                    ILogger<AttractionsController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}