namespace WebApplication1.Utils;

public interface IPasswordHasher
{
    string Hash(string password);

    bool VerifyPassword(string providedPassword, string hashedPassword);
}