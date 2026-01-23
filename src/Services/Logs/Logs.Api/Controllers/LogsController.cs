using AutoMapper;
using EventBus.Messages.Events;
using Logs.Core.Contracts.Persistence;
using Logs.Domain.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Logs.Api.Controllers
{
    [Route("api/v1/histories")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        #region constructor
        private readonly IHistoryRepository _historyRepository;
        private readonly ILogger<HistoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publisher;
        public HistoryController(IHistoryRepository historyRepository, ILogger<HistoryController> logger,
            IMapper mapper, IPublishEndpoint publisher)
        {
            _historyRepository = historyRepository;
            _logger = logger;
            _mapper = mapper;
            _publisher = publisher;
        }
        #endregion

        #region get histories
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<History>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<History>?>> GetHistories()
        {
            var histories = await _historyRepository.GetHistoriesAsync();
            return Ok(histories);
        }
        #endregion

        #region get histories
        [HttpGet("{userName}/{section?}/{recordId?}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<History>?>> GetHistories(string userName, string? section, string? recordId)
        {
            var histories = await _historyRepository.GetHistoriesAsync(userName, section, recordId);
            if (histories == null)
            {
                _logger.LogError($"History is not found");
                return NotFound();
            }

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

        #region create history
        [HttpPost]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<History>> CreateHistory([FromBody] History history)
        {
            await _historyRepository.CreateHistoryAsync(history);
            return CreatedAtRoute("GetHistory", new { id = history.Id }, history);
        }
        #endregion

        #region update history
        [HttpPut]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateHistory([FromBody] History history)
        {
            return Ok(await _historyRepository.UpdateHistoryAsync(history));
        }
        #endregion

        #region delete history
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(History), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteHistory(string id)
        {
            return Ok(await _historyRepository.DeleteHistoryAsync(id));
        }
        #endregion

        #region publish
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Publish([FromBody] LogsHistoryPublish logs)
        {
            ////get existing...
            //var history = _historyRepository.GetHistoriesAsync(logs.UserName, logs.Section, logs.RecordId);
            //if (history == null)
            //    return BadRequest();

            //create event
            var eventMessage = _mapper.Map<LogsHistoryEvent>(logs);

            //send event to rabbitmq
            await _publisher.Publish(eventMessage);

            return Accepted();
        }
        #endregion
    }
}
