using System.ComponentModel.DataAnnotations;
namespace TestHub.Core.Dtos.AuthDTO;

public class VerifiedEmaildDto
{
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
}