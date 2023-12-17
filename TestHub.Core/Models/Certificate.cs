namespace TestHub.Core.Models;

public class Certificate
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string CertificateNumber { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public int OwnerId { get; set; }
    public virtual User Owner { get; set; } = null!;
}