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

        //GET: api/attractions/readattractions
        [HttpGet()]
        [ActionName("ReadAttractions")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<AttractionReadListDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractions
            (string seeded = "true", string flat = "false", string filter = null,
            string pageNr = "0", string pageSz = "10")
        {
            try
            {
                bool seededArg = bool.Parse(seeded);
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSzArg = int.Parse(pageSz);

                _logger.LogInformation($"{nameof(ReadAttractions)}: {nameof(seededArg)} - {seededArg}, " +
                    $"{nameof(flatArg)} - {flatArg}, {nameof(pageNrArg)} - {pageNrArg}, " +
                    $"{nameof(pageSzArg)} - {pageSzArg} ");

                var res = await _service.ReadAttractionsAsync(seededArg, flatArg, filter?.Trim().ToLower(),
                    pageNrArg, pageSzArg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadAttractions)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //GET: api/attractions/nocomments
        [HttpGet()]
        [ActionName("NoComments")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<AttractionNoCommentDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> NoComments(string pageNr = "0", string pageSz = "10")
        {
            try
            {
                int pageNrArg = int.Parse(pageNr);
                int pageSzArg = int.Parse(pageSz);

                _logger.LogInformation($"{nameof(NoComments)}: {nameof(pageNrArg)} - {pageNrArg}, " +
                    $"{nameof(pageSzArg)} - {pageSzArg}");

                var res = await _service.ReadAttractionsWithoutReviewAsync(pageNrArg, pageSzArg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(NoComments)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //GET: api/attractions/readitem
        [HttpGet()]
        [ActionName("ReadItem")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadItem)}: {nameof(idArg)} - {idArg}, {nameof(flatArg)} - {flatArg}");

                var item = await _service.ReadAttractionAsync(idArg, flatArg);
                if (item == null) throw new ArgumentException($"Item {id} does not exist");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItem)}: {ex.Message} - {ex.InnerException}");
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