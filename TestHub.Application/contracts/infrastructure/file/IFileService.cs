namespace Application.contracts.infrastructure.file;

public interface IFileService
{
    Task<string> UploadImage(FileDto file);
}