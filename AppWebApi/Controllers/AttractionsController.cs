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

        public AttractionsController(IAttractionsService service,
                                    ILogger<AttractionsController> logger)
        {
            _service = service;
            _logger = logger;
        }

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

        //GET: api/attractions/readattraction
        [HttpGet()]
        [ActionName("ReadAttraction")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadAttraction(string id = null, string flat = "false")
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadAttraction)}: {nameof(idArg)} - {idArg}, {nameof(flatArg)} - {flatArg}");

                var item = await _service.ReadAttractionAsync(idArg, flatArg);
                if (item == null) throw new ArgumentException($"Item {id} does not exist");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadAttraction)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //POST: api/users/createAttraction
        [HttpPost()]
        [ActionName("CreateAttraction")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<AttractionReadItemDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateAttraction([FromBody] AttractionCuDto item)
        {
            try
            {
                item.EnsureValidity();

                _logger.LogInformation($"{nameof(CreateAttraction)}");

                var _item = await _service.CreateAttractionAsync(item);

                _logger.LogInformation($"Item {_item.Item.AttractionId} created");

                return Ok(_item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateAttraction)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"Could not create item. Error {ex.Message} - {ex.InnerException}");
            }
        }

        //PUT: api/friends/updateattraction
        [HttpPut("{id}")]
        [ActionName("UpdateAttraction")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<AttractionReadItemDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateAttraction(string id, [FromBody] AttractionCuDto item)
        {
            try
            {
                item.EnsureValidity();

                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(UpdateAttraction)}: {nameof(idArg)} - {idArg}");

                if (item.AttractionId != idArg) throw new ArgumentException($"Attraction {id} does not exist");
                var _item = await _service.UpdateAttractionAsync(item);

                _logger.LogInformation($"item {idArg} updated");

                return Ok(_item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateAttraction)}: {ex.Message}");
                return BadRequest($"Could not update. Error {ex.Message}");
            }
        }

        //DELETE: api/users/deleteattraction
        [HttpDelete("{id}")]
        [ActionName("DeleteAttraction")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteAttraction(string id)
        {
            try
            {
                Guid idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(DeleteAttraction)}: {nameof(idArg)} - {nameof(idArg)}");

                var item = await _service.DeleteAttractionAsync(idArg);
                if (item == null)
                    throw new ArgumentException($"Attraction with id {id} does not exist");

                _logger.LogInformation($"Attraction {idArg} is deleted");

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteAttraction)}: {ex.Message} - {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }
    }
}