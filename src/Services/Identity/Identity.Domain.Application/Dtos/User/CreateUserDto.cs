using Identity.Domain.Core.AggregateModels.Users;

namespace Identity.Domain.Application.Dtos.User
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
