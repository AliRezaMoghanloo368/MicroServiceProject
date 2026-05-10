using Identity.Domain.Core.AggregateModels.UserItems;
using Identity.Domain.Core.Interfaces;
using Identity.Domain.Infra.Data.Context;
using Identity.Domain.Infra.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SharedLibrary.Encryptor;

namespace WebAppSUT.IntegrationTest
{
    public class BaseTest //: IDisposable
    {
        protected WebApplicationFactory<Program> _factory;
        protected IServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<IdentityContext>));
                    services.AddDbContext<IdentityContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryIdentityApi");
                    });
                    services.AddSingleton<IEncryptor, Encryptor>();
                    services.AddScoped<IUserRepository, UserRepository>();
                });
            });
            _serviceProvider = _factory.Services;
        }

        [SetUp]
        public void SetUp()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var dbContext = scopeService.GetRequiredService<IdentityContext>();
                try
                {
                    dbContext.Database.EnsureCreated();
                    var fileInfo = UserEntity.CreateUserInfo("AliRezaMoghanloo",
                                                       "09195438781",
                                                       "AliReza.Moghanloo368@Gmail.Com");
                    var user = UserEntity.CreateUser("Test",
                                               "1234",
                                               fileInfo);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    //log
                }
            }
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _factory.Dispose();
        }

        protected T GetService<T>() => _serviceProvider.GetRequiredService<T>();
    }
}
