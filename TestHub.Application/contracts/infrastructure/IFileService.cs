namespace Application.contracts.infrastructure;

public interface IFileService
{
    Task<string> UploadImage(FileDto file);
}