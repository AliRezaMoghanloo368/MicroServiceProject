using Files.Domain.Models;

namespace Files.Application.Interfaces
{
    public interface IFilesRepository
    {
        Task<FilesEntity?> GetByIdAsync(string id);
        Task<List<FilesEntity>> GetFilesAsync(string entityName, string entityId);
        Task<FilesEntity> CreateAsync(FilesEntity entity);
        Task UpdateAsync(FilesEntity entity);
        Task DeleteAsync(FilesEntity entity);
    }
}
