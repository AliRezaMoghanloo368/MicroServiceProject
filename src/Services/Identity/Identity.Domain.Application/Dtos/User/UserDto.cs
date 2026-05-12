using Identity.Domain.Core.AggregateModels.UserItems;

namespace Identity.Domain.Application.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
