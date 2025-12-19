using Logs.Core.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Logs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        #region constructor
        private readonly ILogsRepository _logsRepository;
        private readonly ILogger<LogsController> _logger;
        public LogsController(ILogsRepository logsRepository, ILogger<LogsController> logger)
        {
            _logsRepository = logsRepository;
            _logger = logger;
        }
        #endregion

        #region get logs
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Logs>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Logs>>> GetLogs()
        {
            var logs = await _logsRepository.GetLogs();
            return Ok(logs);
        }

        #endregion

        #region get logs by id
        [HttpGet("{id:length(24)}", Name = "GetLogs")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Logs), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Logs>>> GetLogs(string id)
        {
            var logs = await _logsRepository.GetLog(id);
            if (logs == null)
            {
                _logger.LogError($"logs with id: {id} is not found");
                return NotFound();
            }

            return Ok(logs);
        }
        #endregion

        #region get logs by category name
        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Logs>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Logs>>> GetLogsByCategory(string category)
        {
            var logs = await _logsRepository.GetLogsByCategory(category);
            return Ok(logs);
        }
        #endregion

        #region create logs
        [HttpPost]
        [ProducesResponseType(typeof(Logs), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Logs>> CreateLogs([FromBody] Logs logs)
        {
            await _logsRepository.CreateLogs(logs);
            return CreatedAtRoute("GetLogs", new { id = logs.Id }, logs);
        }
        #endregion

        #region update logs
        [HttpPut]
        [ProducesResponseType(typeof(Logs), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateLogs([FromBody] Logs logs)
        {
            return Ok(await _logsRepository.UpdateLogs(logs));
        }
        #endregion

        #region delete logs
        [HttpDelete("{id:length(24)}", Name = "DeleteLogs")]
        [ProducesResponseType(typeof(Logs), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteLogs(string id)
        {
            return Ok(await _logsRepository.DeleteLogs(id));
        }
        #endregion
    }
}
