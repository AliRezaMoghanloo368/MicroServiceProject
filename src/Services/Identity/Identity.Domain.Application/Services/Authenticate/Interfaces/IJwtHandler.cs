using Identity.Domain.Core.AggregateModels.UserItems;

namespace Identity.Domain.Application.Services.Authenticate.Interfaces
{
    public interface IJwtHandler
    {
        public JsonWebToken Create(User user);
    }
}
