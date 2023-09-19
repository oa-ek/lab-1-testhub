using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class CategoryDto
{
    [Required] [MaxLength(512)] public string Title { get; set; } = null!;
}