using Application.contracts.infrastructure.file;
using Application.dtos.sharedDTOs;

namespace TestHub.Infrastructure.services.file;

public class FileService : IFileService
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