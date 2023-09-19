using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class UserDto
{
    [Required] [MaxLength(255)] public string Email { get; set; } = null!;

    [Required] [MaxLength(255)] public string Password { get; set; } = null!;

    [Required] [MaxLength(255)] public string Name { get; set; } = null!;
}