using AutoMapper;
using EventBus.Messages.Events;
using Logs.Domain.Models;

namespace Logs.Core.Mapping
{
    public class LogsMappingProfiler : Profile
    {
        public LogsMappingProfiler()
        {
            CreateMap<History, LogsHistoryEvent>().ReverseMap();
        }
    }
}
