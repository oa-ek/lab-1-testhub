namespace Application.contracts.infrastructure.file;

public interface ICloudFileStorage
{
    Task<string> UploadToCloud(FileStream stream, string fileName);
}