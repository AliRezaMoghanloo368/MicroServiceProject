using Identity.Domain.Core.AggregateModels.UserItems;
using Identity.Domain.Core.Interfaces;
using Shouldly;
using System.Net.Http.Json;
using WebAppSUT.Helper;

namespace WebAppSUT.IntegrationTest
{
    public class IntegrationTest : BaseTest
    {
        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _userRepository = GetService<IUserRepository>();
        }

        [Test]
        public async Task Integration()
        {
            var client = _factory.CreateClient();
            var user = await client.GetAsync(Urls.Identity.UserNameUrl.Replace("{userName}", "Test"));
            var result = await user.Content.ReadFromJsonAsync<List<UserEntity>>();
            result.ShouldNotBeNull();
            var ormUsers = await _userRepository.GetAllAsync();
            result[0].UserName.ShouldBe(ormUsers[0].UserName);
        }
    }
}
