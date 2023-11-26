namespace Application.dtos.sharedDTOs;

public class FileDto
{
    public required string FileName { get; set; }  
    public required string ContentType { get; set; }  
    public required byte[] Data { get; set; } 
}