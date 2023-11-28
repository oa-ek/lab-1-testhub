namespace TestHub.API.services;

public class FileService
{
    private readonly ILocalFileStorage _localFileStorage;
    private readonly ICloudFileStorage _cloudFileStorage;

    public FileService(ILocalFileStorage localFileStorage, ICloudFileStorage cloudFileStorage)
    {
        _localFileStorage = localFileStorage;
        _cloudFileStorage = cloudFileStorage;
    }

    public async Task<string> UploadImage(FileDto file)
    {
        var localLink = await _localFileStorage.SaveLocal(file);
        
        var cloudLink = await _cloudFileStorage.UploadToCloud(new FileStream(localLink.Item1, FileMode.Open), localLink.Item2);

        return cloudLink;
    }
}