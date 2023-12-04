using System.ComponentModel.DataAnnotations;

namespace TestHub.API.models.identity;

public class RegistrationRequest
{
    [Required]
    [MinLength(6)]
    public string UserName { get; set; }  = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }  = null!;
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }  = null!;
}