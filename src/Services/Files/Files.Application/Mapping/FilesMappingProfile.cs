using AutoMapper;
using Files.Application.Dtos;
using Files.Domain.Models;

namespace Files.Application.Mapping
{
    public class FilesMappingProfile : Profile
    {
        public FilesMappingProfile()
        {
            CreateMap<FilesEntity, FilesDto>().ReverseMap();
        }
    }
}
