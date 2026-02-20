using AutoMapper;
using EventBus.Messages.Events;
using Logs.Core.Contracts.Persistence;
using Logs.Domain.Models;
using MassTransit;

namespace Logs.Api.EventBusConsumer
{
    public class LogsHistoryConsumer : IConsumer<LogsHistoryEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LogsHistoryConsumer> _logger;
        private readonly IHistoryRepository _historyRepository;
        public LogsHistoryConsumer(IMapper mapper, ILogger<LogsHistoryConsumer> logger, IHistoryRepository historyRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _historyRepository = historyRepository;
        }

        public async Task Consume(ConsumeContext<LogsHistoryEvent> context)
        {
            var history = _mapper.Map<History>(context.Message);
            await _historyRepository.CreateHistoryAsync(history);
            _logger.LogInformation($"history consumed successfully and history id is : {history.Id}");
        }
    }
}
