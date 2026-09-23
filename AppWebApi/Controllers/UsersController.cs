using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Models;
using Models.DTO;
using Services;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        readonly IUsersService _service;
        ILogger<UsersController> _logger;

        //GET: api/users/readusers
        [HttpGet()]
        [ActionName("ReadUsers")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<UserReadDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUsers(string pageNr = "0", string pageSz = "10")
        {
            try
            {
                int pageNrArg = int.Parse(pageNr);
                int pageSzArg = int.Parse(pageSz);

                _logger.LogInformation($"{nameof(ReadUsers)}: {nameof(pageNrArg)} - {pageNrArg}, {nameof(pageSzArg)} - {pageSzArg}");
                var res = await _service.ReadUsersAsync(pageNrArg, pageSzArg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadUsers)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //GET: api/users/readuser
        [HttpGet()]
        [ActionName("ReadUser")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<UserReadDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUser(string id = null, string flat = "false")
        {
            try
            {
                Guid idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadUser)}: {nameof(idArg)} - {idArg}, {nameof(flatArg)} - {flatArg}");
                var res = await _service.ReadUserAsync(idArg, flatArg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadUser)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //POST: api/users/createitem
        [HttpPost()]
        [ActionName("CreateItem")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IUser>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateItem([FromBody] UserCuDto item)
        {
            try
            {
                item.EnsureValidity();

                _logger.LogInformation($"{nameof(CreateItem)}");

                var _item = await _service.CreateUserAsync(item);

                _logger.LogInformation($"Item {_item.Item.UserId} created");

                return Ok(_item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateItem)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"Could not create item. Error {ex.Message} - {ex.InnerException}");
            }
        }

        public UsersController(IUsersService service,
                                    ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}