using Identity.Domain.Core.Common.SeedWork.Implements;
using SharedLibrary.Encryptor;

namespace Identity.Domain.Core.AggregateModels.UserItems
{
    public class UserEntity : AggregateRoot<UserId>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreateAt { get; set; }
        public UserInfo UserInfo { get; set; }

        private UserEntity() { }

        private UserEntity(IEncryptor encryptor, UserId id, string userName, string password, UserInfo userInfo)
        {
            Id = id;
            UserName = userName;
            CreateAt = DateTime.Now;
            Salt = encryptor.GetSalt();
            Password = encryptor.GetHash(password, Salt);
            UserInfo = userInfo;
        }

        public static UserInfo CreateUserInfo(string fullName, string phoneNumber, string? email)
        {
            return new UserInfo(fullName, phoneNumber, email);
        }

        public static UserEntity CreateUser(IEncryptor encryptor, string userName, string password, UserInfo userInfo)
        {
            var id = Guid.NewGuid();
            return new UserEntity(encryptor, new UserId(id), userName, password, userInfo);
        }

        public static UserEntity Reconstitute(
            UserId id, string userName, string password, string salt, DateTime createAt, UserInfo userInfo) 
        {
            return new UserEntity
            {
                Id = id,
                UserName = userName,
                Password = password,
                Salt = salt,
                CreateAt = createAt,
                UserInfo = userInfo
            };
        }

        public bool ValidatePassword(string password, IEncryptor encryptor)
        => Password.Equals(encryptor.GetHash(password, Salt));
    }
}
