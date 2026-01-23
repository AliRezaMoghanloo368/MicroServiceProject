using AutoMapper;
using EventBus.Messages.Events;
using Main.Application.Dtos.Histories;
using MassTransit;
using MediatR;

namespace Main.Application.EventBusConsumer
{
    public class LogsHistoryConsumer : IConsumer<LogsHistoryEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<LogsHistoryConsumer> _logger;
        public LogsHistoryConsumer(IMapper mapper, IMediator mediator, ILogger<LogsHistoryConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<LogsHistoryEvent> context)
        {
            var command = _mapper.Map<HistoryDto>(context.Message);
            var result = await _mediator.Send(command);
            _logger.LogInformation($"history consumed successfully and history id is : {result}");
        }
    }
}
