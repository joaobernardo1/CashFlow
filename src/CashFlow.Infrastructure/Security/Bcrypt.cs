using CashFlow.Domain.Security.Criptography;

namespace CashFlow.Infrastructure.Security
{
    internal class Bcrypt : IPasswordEncrypter

    {
        public string Encrypt(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }
    }
}
