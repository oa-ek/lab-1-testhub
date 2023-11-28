namespace Application.contracts.infrastructure;

public interface ICloudFileStorage
{
    Task<string> UploadToCloud(FileStream stream, string fileName);
}