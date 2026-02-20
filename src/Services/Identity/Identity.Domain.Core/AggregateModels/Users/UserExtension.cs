using SharedLibrary.Encryptor;
using SharedLibrary.Exceptions;

namespace Identity.Domain.Core.AggregateModels.Users
{
    public static class UserExtension
    {
        public static string HashPassword(this string password, string salt, IEncryptor encryptor)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioException("empty_password",
                    "Password can not be empty.");
            }

            return encryptor.GetHash(password, salt);
        }

        public static string GenerateSalt(this string salt, IEncryptor encryptor)
        {
            return encryptor.GetSalt();
        }
    }
}
