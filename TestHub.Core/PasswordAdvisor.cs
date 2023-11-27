using System;
using System.Linq;

public class PasswordAdvisor
{
    public enum PasswordScore
    {
        Blank,
        VeryWeak,
        Weak,
        Strong,
        VeryStrong
    }

    public static PasswordScore CheckStrength(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return PasswordScore.Blank;
        }

        int score = 0;
        bool hasUpperCase = false;
        bool hasLowerCase = false;
        bool hasDigit = false;
        bool hasSpecialCharacter = false;

        foreach (char c in password)
        {
            if (char.IsDigit(c))
            {
                hasDigit = true;
            }
            else if (char.IsUpper(c))
            {
                hasUpperCase = true;
            }
            else if (char.IsLower(c))
            {
                hasLowerCase = true;
            }
            else if (char.IsSymbol(c) || char.IsPunctuation(c))
            {
                hasSpecialCharacter = true;
            }
        }

        if (hasDigit) score++;
        if (hasUpperCase) score++;
        if (hasLowerCase) score++;
        if (hasSpecialCharacter) score++;

        if (password.Length < 8)
        {
            return (PasswordScore)Math.Max(score, (int)PasswordScore.Weak);
        }
        else if (password.Length < 12)
        {
            return (PasswordScore)Math.Max(score, (int)PasswordScore.Strong);
        }
        else
        {
            return (PasswordScore)Math.Max(score, (int)PasswordScore.VeryStrong);
        }
    }
}