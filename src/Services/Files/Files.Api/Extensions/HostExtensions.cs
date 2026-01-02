using Npgsql;

namespace Files.Api.Extensions
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

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Files";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Files(Id SERIAL PRIMARY KEY,
                                                                EntityName VARCHAR(200) NOT NULL,
                                                                EntityId TEXT NOT NULL,
                                                                FileContent BYTEA,
                                                                UploadAt TIMESTAMP)";

                    command.ExecuteNonQuery();

                    // seed data
                    //command.CommandText = "INSERT INTO Files(EntityName, EntityId, FileContent, UploadAt) VALUES ('Students', '', @FileContent, NOW());";
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
