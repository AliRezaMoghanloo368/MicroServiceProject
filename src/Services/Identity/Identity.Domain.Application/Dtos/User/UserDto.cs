using Identity.Domain.Core.AggregateModels.UserItems;

namespace Identity.Domain.Application.Dtos.User
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
