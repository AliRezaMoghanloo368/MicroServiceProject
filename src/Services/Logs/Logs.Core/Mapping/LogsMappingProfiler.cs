namespace Logs.Core.Mapping
{
    public class LogsMappingProfiler : Profile
    {
        public LogsMappingProfiler()
        {
            CreateMap<HistoryDto, CreateHistoryRequest>().ReverseMap();
        }
    }
}
