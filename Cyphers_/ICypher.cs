namespace Cyphers
{
    public interface ICypher
    {
        string Decrypt(string message);
        string Encrypt(string message);
    }
}
