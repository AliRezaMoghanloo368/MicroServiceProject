using Identity.Domain.Core.Common.SeedWork.Implements;
using SharedLibrary.Encryptor;

namespace Identity.Domain.Core.AggregateModels.Users
{
    public class User : AggregateRoot<UserId>
    {
        private readonly IEncryptor _encryptor;

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreateAt { get; set; }
        public UserInfo UserInfo { get; set; }

        private User() { }

        private User(UserId id, string userName, string password, UserInfo userInfo)
        {
            Id = id;
            UserName = userName;
            CreateAt = DateTime.Now;
            Salt = _encryptor.GetSalt();
            Password = _encryptor.GetHash(password, Salt);
        }

        public static UserInfo CreateUserInfo(string fullName, string phoneNumber, string? email)
        {
            return new UserInfo(fullName, phoneNumber, email);
        }

        public static User CreateUser(string userName, string password, UserInfo userInfo)
        {
            var id = Guid.NewGuid();
            return new User(new UserId(id), userName, password, userInfo);
        }

        public bool ValidatePassword(string password, IEncryptor encryptor)
        => Password.Equals(encryptor.GetHash(password, Salt));
    }
}
