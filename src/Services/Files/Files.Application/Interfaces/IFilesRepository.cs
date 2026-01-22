using Files.Application.Dtos;
using Files.Domain.Models;

namespace Files.Application.Interfaces
{
    public interface IFilesRepository
    {
        Task<FilesEntity?> GetByIdAsync(Guid id);
        Task<List<FilesEntity>> GetFilesAsync(string entityName, string entityId);
        Task<FilesEntity> CreateAsync(FilesEntity entity);
        Task<bool> UpdateAsync(FilesEntity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
