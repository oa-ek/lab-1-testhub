﻿using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class TestDto
{
    [Required] [MaxLength(255)] public string Title { get; set; } = null!;
    
    [MaxLength(512)] public string? Description { get; set; }
    
    [Required] public int Duration { get; set; }
    
    [Required] public string[] Categories { get; set; }
    
    [Required] public bool IsPublic { get; set; }
    
    [Required] public string Status { get; set; } = null!;
    
}