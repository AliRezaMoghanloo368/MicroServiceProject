using Identity.Domain.Core.Interfaces;
using Identity.Domain.Infra.Data.Context;
using Identity.Domain.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Encryptor;

namespace WebAppSUT.IntegrationTest
{
    public class StartUp
    {
        private static readonly ServiceProvider _serviceProvider;
        static StartUp()
        {
            IServiceCollection services = new ServiceCollection();

            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(projectPath).AddJsonFile("appsetting.json").Build();
            services.AddDbContext<IdentityContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("Root")));

            services.AddSingleton<IEncryptor, Encryptor>();
            services.AddScoped<IUserRepository, UserRepository>();
            _serviceProvider = services.BuildServiceProvider();
        }

        protected static T GetService<T>() => _serviceProvider.GetRequiredService<T>();
    }
}
