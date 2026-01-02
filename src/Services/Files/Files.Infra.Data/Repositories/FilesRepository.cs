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
            connectionString = _configuration.GetSection("ConnectionStrings:Root").Value;
        }
        #endregion

        #region Get File
        public async Task<FilesEntity?> GetByIdAsync(string id)
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

            var affected = await connection.QuerySingleAsync
                ("INSERT INTO FilesEntity (EntityName, EntityId, FileContent) VALUES (@EntityName, @EntityId, @FileContent)",
                new { EntityName = entity.EntityName, EntityId = entity.EntityId, FileContent = entity.FileContent });

            return affected;
        }
        #endregion

        #region Update 
        public async Task UpdateAsync(FilesEntity entity)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var affected = await connection.ExecuteAsync
                ("UPDATE FilesEntity SET EntityName=@EntityName, EntityId=@EntityId, FileContent=@FileContent WHERE Id=@Id",
                new { EntityName = entity.EntityName, EntityId = entity.EntityId, FileContent = entity.FileContent, Id = entity.Id });
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(FilesEntity entity)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var affected = await connection.ExecuteAsync
                ("DELETE FROM FilesEntity WHERE Id=@Id",
                new { Id = entity.Id });
        }
        #endregion
    }
}
