using AutoMapper;
using Files.Application.Dtos;
using Files.Domain.Models;

namespace Files.Application.Mapping
{
    public class FilesMappingProfile : Profile
    {
        public FilesMappingProfile()
        {
            CreateMap<UpdateFileDto, FilesEntity>()
                .ForMember(dest => dest.FileContent, opt => opt.Ignore());

            CreateMap<FilesDto, FilesEntity>().ReverseMap();

            CreateMap<CreateFileDto, FilesEntity>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.UploadAt, o => o.Ignore());
        }
    }
}
