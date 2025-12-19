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
            var histories = await _historyRepository.GetHistoryAsync();
            return Ok(histories);
        }

        #endregion

        #region get histories by id
        [HttpGet("{id:length(24)}", Name = "GetHistories")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<History>>> GetHistories(string id)
        {
            var histories = await _historyRepository.GetHistoryAsync(id);
            if (histories == null)
            {
                _logger.LogError($"histories with id: {id} is not found");
                return NotFound();
            }

            return Ok(histories);
        }
        #endregion

        //#region get histories by category name
        //[HttpGet("[action]/{category}")]
        //[ProducesResponseType(typeof(IEnumerable<History>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<History>>> GetHistoriesByCategory(string category)
        //{
        //    var histories = await _historiesRepository.GetHistoryByCategoryAsync(category);
        //    return Ok(histories);
        //}
        //#endregion

        #region create histories
        [HttpPost]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<History>> CreateHistories([FromBody] History histories)
        {
            await _historyRepository.CreateHistoryAsync(histories);
            return CreatedAtRoute("GetHistories", new { id = histories.Id }, histories);
        }
        #endregion

        #region update histories
        [HttpPut]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateHistories([FromBody] History histories)
        {
            return Ok(await _historyRepository.UpdateHistoryAsync(histories));
        }
        #endregion

        #region delete histories
        [HttpDelete("{id:length(24)}", Name = "DeleteHistories")]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteHistories(string id)
        {
            return Ok(await _historyRepository.DeleteHistoryAsync(id));
        }
        #endregion
    }
}
