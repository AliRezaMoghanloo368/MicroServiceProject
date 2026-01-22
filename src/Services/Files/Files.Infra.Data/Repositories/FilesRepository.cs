using Dapper;
using Files.Application.Interfaces;
using Files.Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Dapper.SqlMapper;

namespace Files.Infra.Data.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        #region Constructor
        private string? connectionString;
        private readonly IConfiguration _configuration;
        public FilesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("DatabaseSettings:Root").Value;
        }
        #endregion

        #region Get File
        public async Task<FilesEntity?> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(connectionString);

            return await connection.QuerySingleOrDefaultAsync<FilesEntity>(
                "SELECT * FROM Files WHERE Id = @Id",
                new { Id = id }
            );
        }
        #endregion

        #region Get Files
        public async Task<List<FilesEntity>> GetFilesAsync(string entityName, string entityId)
        {
            using var connection = new NpgsqlConnection(connectionString);

            // QueryAsync خودش IEnumerable برمی‌گردونه
            var affected = await connection.QueryAsync<FilesEntity>(
                "SELECT * FROM Files WHERE EntityId = @EntityId AND EntityName = @EntityName",
                new { EntityId = entityId, EntityName = entityName }
            );

            // تبدیل به List
            return affected.ToList();
        }
        #endregion

        #region Create
        public async Task<FilesEntity> CreateAsync(FilesEntity entity)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var affected = await connection.QuerySingleAsync<FilesEntity>
                ("INSERT INTO Files (Id, EntityName, EntityId, FileContent, UploadAt) VALUES (@Id, @EntityName, @EntityId, @FileContent, @UploadAt) RETURNING *;",
                new { Id = entity.Id, EntityName = entity.EntityName, EntityId = entity.EntityId, FileContent = entity.FileContent, UploadAt = entity.UploadAt });

            return affected;
        }
        #endregion

        #region Update 
        public async Task<bool> UpdateAsync(FilesEntity entity)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var affected = await connection.ExecuteAsync
                ("UPDATE Files SET EntityName=@EntityName, EntityId=@EntityId, FileContent=@FileContent WHERE Id=@Id",
                new { EntityName = entity.EntityName, EntityId = entity.EntityId, FileContent = entity.FileContent, Id = entity.Id });

            return affected == 0 ? false : true;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var affected = await connection.ExecuteAsync
                ("DELETE FROM Files WHERE Id=@Id",
                new { Id = id });

            return affected == 0 ? false : true;
        }
        #endregion
    }
}
