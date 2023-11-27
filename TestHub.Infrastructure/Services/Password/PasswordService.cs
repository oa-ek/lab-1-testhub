using Microsoft.Extensions.Configuration;

namespace TestHub.Infrastructure.Services.Password
{
    public class PasswordService
    {
        private IConfiguration _configuration;

        public PasswordService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GeneratePasswordResetCode()
        {
            int defaultCodeLength = GetDefaultCodeLength();
            return PasswordResetCodeGenerator.GenerateCode(defaultCodeLength);
        }

        public string GeneratePasswordResetCode(int codeLength)
        {
            return PasswordResetCodeGenerator.GenerateCode(codeLength);
        }

        public (bool IsValid, string ErrorMessage) ValidatePassword(string password)
        {
            PasswordAdvisor.PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(password);

            switch (passwordStrengthScore)
            {
                case PasswordAdvisor.PasswordScore.Blank:
                case PasswordAdvisor.PasswordScore.VeryWeak:
                case PasswordAdvisor.PasswordScore.Weak:
                    return (false, $"Password is {passwordStrengthScore}.");
                default:
                    return (true, "Password meets requirements.");
            }
        }

        private int GetDefaultCodeLength()
        {
            return int.TryParse(_configuration["EnvironmentalVariables:DefaultResetCodeLength"], out var defaultCodeLength) ? defaultCodeLength : 8;
        }
    }
}