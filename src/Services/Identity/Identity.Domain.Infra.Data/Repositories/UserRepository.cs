using Dapper;
using Identity.Domain.Core.AggregateModels.UserItems;
using Identity.Domain.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SharedLibrary.Patterns.ResultPattern;
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
                @"SELECT * FROM ""MGH"".""Users"" WHERE ""Id"" = @Id",
                new { Id = id }
            );
        }

        public async Task<UserEntity?> GetByUserNameAsync(string name)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var user = await connection.QuerySingleOrDefaultAsync<dynamic>(
                @"SELECT * FROM ""MGH"".""Users"" WHERE ""UserName"" = @UserName",
                new { UserName = name }
            );

            if (user == null)
                return null;

            var userInfo = await connection.QuerySingleOrDefaultAsync<dynamic>(
                @"SELECT * FROM ""MGH"".""UserInfo"" WHERE ""UserId"" = @UserId",
                new { UserId = user.Id }
            );

            var createUserInfo = UserInfo.Create(userInfo.FullName, userInfo.PhoneNumber, userInfo.Email);

            // اگر بخواهی Entity را از DB بازسازی کنی، باید یک factory/reconstitution method داشته باشی
            return UserEntity.Reconstitute(
                new UserId(user.Id),
                user.UserName,
                user.Password,
                user.Salt,
                user.CreateAt,
                createUserInfo
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
            command.CommandText = @"WITH generated_uuid AS (
                                        SELECT gen_random_uuid() AS new_uuid
                                    ), 
                                    InsertedUser AS (
                                        INSERT INTO ""MGH"".""Users"" (""Id"", ""UserName"", ""Password"", ""Salt"", ""CreateAt"")
                                        SELECT new_uuid, @UserName, @Password, @Salt, @CreateAt
                                        FROM generated_uuid
                                        RETURNING ""Id""
                                    )
                                    INSERT INTO ""MGH"".""UserInfo"" (""FullName"", ""PhoneNumber"", ""Email"", ""UserId"")
                                    SELECT @FullName, @PhoneNumber, @Email, ""Id"" 
                                    FROM InsertedUser;";
            command.Parameters.AddWithValue("UserName", entity.UserName);
            command.Parameters.AddWithValue("Password", entity.Password);
            command.Parameters.AddWithValue("Salt", entity.Salt);
            command.Parameters.AddWithValue("FullName", entity.UserInfo.FullName);
            command.Parameters.AddWithValue("PhoneNumber", entity.UserInfo.PhoneNumber);
            command.Parameters.AddWithValue("Email", entity.UserInfo.Email);
            command.Parameters.AddWithValue("CreateAt", entity.CreateAt);
            await command.ExecuteNonQueryAsync();

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
            const string query = @"
                SELECT EXISTS(
                    SELECT 1
                    FROM ""MGH"".""Users""
                    WHERE ""UserName"" = @UserName
                );";

            await using var connection = new NpgsqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<bool>(query, new { UserName = name });
        }
        #endregion
    }
}
