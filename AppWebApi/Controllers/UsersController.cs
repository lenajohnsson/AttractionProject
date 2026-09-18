using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IUsersService service,
                                    ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}