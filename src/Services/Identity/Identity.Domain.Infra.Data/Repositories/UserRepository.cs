using Dapper;
using Identity.Domain.Core.AggregateModels.UserItems;
using Identity.Domain.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Identity.Domain.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Constructor
        private string? connectionString;
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("DatabaseSettings:Root").Value;
        }
        #endregion

        #region Get File
        public async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(connectionString);

            return await connection.QuerySingleOrDefaultAsync<UserEntity>(
                "SELECT * FROM IdentityDB WHERE Id = @Id",
                new { Id = id }
            );
        }

        public async Task<UserEntity?> GetByUserNameAsync(string name)
        {
            using var connection = new NpgsqlConnection(connectionString);

            return await connection.QuerySingleOrDefaultAsync<UserEntity>(
                "SELECT * FROM IdentityDB WHERE UserName = @UserName",
                new { UserName = name }
            );
        }

        public async Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken cancellationToken)
        {
            DataTable dt = new DataTable();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };
            command.CommandText = "INSERT INTO IdentityDB (UserName, Password, Salt, FullName, PhoneNumber, Email, CreateAt) " +
                                                  "VALUES (@UserName, @Password, @Salt, @FullName, @PhoneNumber, @Email, @CreateAt);" +
                                  "SELECT TOP 1 * FROM IdentityDB ORDER BY Id DESC";
            command.Parameters.AddWithValue("UserName", entity.UserName);
            command.Parameters.AddWithValue("Password", entity.Password);
            command.Parameters.AddWithValue("Salt", entity.Salt);
            command.Parameters.AddWithValue("FullName", entity.UserInfo.FullName);
            command.Parameters.AddWithValue("PhoneNumber", entity.UserInfo.PhoneNumber);
            command.Parameters.AddWithValue("Email", entity.UserInfo.Email);
            command.Parameters.AddWithValue("CreateAt", DateTime.UtcNow);
            dt.Load(await command.ExecuteReaderAsync());
            return entity;
        }

        public Task<bool> UpdateAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistUserByUserNameAsync(string name)
        {
            using var connection = new NpgsqlConnection(connectionString);

            return await connection.QuerySingleOrDefaultAsync<bool>(
                "SELECT 1 FROM IdentityDB WHERE UserName = @UserName",
                new { UserName = name }
            );
        }
        #endregion
    }
}
