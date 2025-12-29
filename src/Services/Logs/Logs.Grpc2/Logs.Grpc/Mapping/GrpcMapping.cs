using AutoMapper;
using Logs.Domain.Models;
using Logs.Grpc.Protos;

namespace Logs.Grpc.Mapping
{
    public class GrpcMapping: Profile
    {
        public GrpcMapping()
        {
            CreateMap<HistoryModel, GetHistoriesResponse>().ReverseMap();
            CreateMap<History, CreateHistoryRequest>().ReverseMap();
            CreateMap<History, HistoryModel>().ReverseMap();
        }
    }
}
