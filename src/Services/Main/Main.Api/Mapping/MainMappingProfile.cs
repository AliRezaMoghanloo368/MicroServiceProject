using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Logs.Domain.Models;
using Logs.Grpc.Protos;
using Main.Application.Dtos.Courses;
using Main.Application.Dtos.Histories;
using Main.Domain.Models;

namespace Main.Api.Mapping
{
    public class MainMappingProfile : Profile
    {
        public MainMappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<History, HistoryDto>().ReverseMap();
            CreateMap<History, HistoryModel>().ReverseMap();
            CreateMap<HistoryModel, HistoryDto>().ReverseMap();

        }
    }
}
