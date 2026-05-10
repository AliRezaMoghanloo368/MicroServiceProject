using Npgsql;

namespace Identity.Domain.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                // migrate database
                try
                {
                    logger.LogInformation("migrating posgtresql database");

                    string? connectionString = configuration.GetValue<string>("DatabaseSettings:Root");
                    using var connection = new NpgsqlConnection(connectionString);
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    //command.CommandText = "DROP TABLE IF EXISTS IdentityDB";
                    //command.ExecuteNonQuery();

                    command.CommandText = @"
                        CREATE SCHEMA IF NOT EXISTS ""MGH"";

                        CREATE TABLE IF NOT EXISTS ""MGH"".""Users"" (
                            ""Id""            UUID PRIMARY KEY NOT NULL,
                            ""UserName""      VARCHAR(200) UNIQUE NOT NULL,
                            ""Password""      VARCHAR(200) NOT NULL,
                            ""Salt""          VARCHAR(200) NOT NULL,
                            ""CreateAt""      TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
                        );

                        CREATE TABLE IF NOT EXISTS ""MGH"".""UserInfo"" (
                            ""UserId""        UUID PRIMARY KEY NOT NULL,
                            ""FullName""      VARCHAR(200) NOT NULL,
                            ""PhoneNumber""   VARCHAR(20)  NOT NULL,
                            ""Email""         VARCHAR(200),

                            CONSTRAINT ""FK_UserInfo_Users""
                                FOREIGN KEY (""UserId"")
                                REFERENCES ""MGH"".""Users"" (""Id"")
                                ON DELETE CASCADE
                        );";
                    command.ExecuteNonQuery();

                    // seed data
                    //command.CommandText = "INSERT INTO IdentityDB() VALUES ('Students', '', @FileContent, NOW());";
                    //command.Parameters.AddWithValue("FileContent", new byte[] { 1, 2, 3, 4 });
                    ////command.Parameters.AddWithValue("UploadAt", DateTime.UtcNow);
                    //command.ExecuteNonQuery();

                    logger.LogInformation("migration has been completed!!!");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError("an error has been occured");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }

            return host;
        }
    }
}
