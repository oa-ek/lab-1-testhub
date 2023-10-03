namespace TestHub.Infrastructure.Services.Password;

public class PasswordResetCodeGenerator
{
    private static readonly Random Random = new Random();

    public static string GenerateCode(int codeLength)
    {
        const string validChars = "0123456789";
        char[] codeChars = new char[codeLength];

        for (int i = 0; i < codeLength; i++)
        {
            codeChars[i] = validChars[Random.Next(validChars.Length)];
        }

        return new string(codeChars);
    }
}