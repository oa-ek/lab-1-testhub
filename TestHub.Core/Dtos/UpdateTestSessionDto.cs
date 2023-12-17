using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class UpdateTestSessionDto
{
    [Required] public int Id { get; set; }
    public int? Result { get; set; }
}