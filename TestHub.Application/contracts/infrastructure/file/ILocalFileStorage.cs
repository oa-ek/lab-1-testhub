namespace Application.contracts.infrastructure;

public interface ILocalFileStorage
{
    Task<(string, string)> SaveLocal(FileDto file);
}