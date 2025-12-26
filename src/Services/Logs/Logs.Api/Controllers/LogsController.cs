using Logs.Core.Contracts.Persistence;
using Logs.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Logs.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        #region constructor
        private readonly IHistoryRepository _historyRepository;
        private readonly ILogger<HistoryController> _logger;
        public HistoryController(IHistoryRepository historyRepository, ILogger<HistoryController> logger)
        {
            _historyRepository = historyRepository;
            _logger = logger;
        }
        #endregion

        #region get histories
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<History>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<History>>> GetHistories()
        {
            var histories = await _historyRepository.GetHistoriesAsync();
            return Ok(histories);
        }
        #endregion

        #region get history by id
        [HttpGet("{id:length(24)}", Name = "GetHistory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<History>> GetHistory(string id)
        {
            var history = await _historyRepository.GetHistoryAsync(id);
            if (history == null)
            {
                _logger.LogError($"History with id: {id} is not found");
                return NotFound();
            }

            return Ok(history);
        }
        #endregion

        #region create histories
        [HttpPost]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<History>> CreateHistories([FromBody] History histories)
        {
            await _historyRepository.CreateHistoryAsync(histories);
            return CreatedAtRoute("GetHistory", new { id = histories.Id }, histories);
        }
        #endregion

        #region update history
        [HttpPut]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateHistories([FromBody] History history)
        {
            return Ok(await _historyRepository.UpdateHistoryAsync(history));
        }
        #endregion

        #region delete history
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteHistories(string id)
        {
            return Ok(await _historyRepository.DeleteHistoryAsync(id));
        }
        #endregion
    }
}
