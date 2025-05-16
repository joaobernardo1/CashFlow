namespace CashFlow.Domain.Security.Criptography
{
    public interface IPasswordEncrypter
    {
        string Encrypt(string password);
    }
}
