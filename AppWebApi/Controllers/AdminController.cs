using Microsoft.AspNetCore.Mvc;
using Services;
using Configuration;
using Models.DTO;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        readonly ILogger<AdminController> _logger;
        readonly IAdminService _service;

        public AdminController(ILogger<AdminController> logger, IAdminService service)
        {
            _logger = logger;
            _service = service;
        }

        //GET: api/admin/seed?count={count}
        [HttpGet()]
        [ActionName("Seed")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<GuestUserInfoAllDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Seed(int nrItems = 1000)
        {
            try
            {
                _logger.LogInformation($"{nameof(Seed)}: {nameof(nrItems)}: {nrItems}");
                var info = await _service.SeedAsync(nrItems);

                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Seed)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //GET: api/admin/overview
        [HttpGet()]
        [ActionName("Overview")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<GuestUserInfoAllDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Overview()
        {
            try
            {
                _logger.LogInformation($"{nameof(Overview)}");
                var info = await _service.OverviewAsync();

                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Overview)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //GET: api/admin/removeseed
        [HttpGet]
        [ActionName("RemoveSeed")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<GuestUserInfoAllDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveSeed(string seeded = "true")
        {
            try
            {
                bool seededArg = bool.Parse(seeded);
                _logger.LogInformation($"{nameof(RemoveSeed)}: {nameof(seededArg)}: {seededArg}");
                var info = await _service.RemoveSeedAsync(seededArg);
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(RemoveSeed)}: {ex.Message} - {ex.InnerException}");
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        //GET: api/admin/log
        [HttpGet()]
        [ActionName("Log")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogMessage>))]
        public async Task<IActionResult> Log([FromServices] ILoggerProvider _loggerProvider)
        {
            //Note the way to get the LoggerProvider, not the logger from Services via DI
            if (_loggerProvider is InMemoryLoggerProvider cl)
            {
                return Ok(await cl.MessagesAsync);
            }
            return Ok("No messages in log");
        }
    }
}

