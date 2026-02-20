using Identity.Domain.Core.AggregateModels.Users;

namespace Identity.Domain.Application.Dtos.User
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
