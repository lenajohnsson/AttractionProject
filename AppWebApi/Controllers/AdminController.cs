using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

using Services;
using Configuration;
using Configuration.Options;
using Microsoft.Extensions.Options;
using Models.DTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        readonly ILogger<AdminController> _logger;
        private readonly DbConnectionSetsOptions _dbSetOptions;
        readonly AesEncryptionOptions _aesOptions;
        readonly JwtOptions _jwtOptions;
        readonly VersionOptions _versionOptions;
        readonly IConfiguration _configuration;
        readonly Encryptions _encryptions = null;
        readonly DatabaseConnections _dbConnections = null;
        readonly IAdminService _service;

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


        public AdminController(ILogger<AdminController> logger,
                    IConfiguration configuration,
                    IOptions<DbConnectionSetsOptions> dbSetOptions,
                    IOptions<AesEncryptionOptions> aesOptions,
                    IOptions<JwtOptions> jwtOptions,
                    IOptions<VersionOptions> versionOptions,
                    Encryptions encryptions, DatabaseConnections dbConnections,
                    IAdminService service)
        {
            _logger = logger;

            _dbSetOptions = dbSetOptions.Value;
            _aesOptions = aesOptions.Value;
            _jwtOptions = jwtOptions.Value;
            _versionOptions = versionOptions.Value;
            _configuration = configuration;

            _encryptions = encryptions;
            _dbConnections = dbConnections;

            _service = service;
        }
    }
}

