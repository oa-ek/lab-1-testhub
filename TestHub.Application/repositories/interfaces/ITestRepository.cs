using Domain.entities;

namespace Application.repositories.interfaces;

public interface ITestRepository
{
    public Task<IEnumerable<Test>> GetByOwnerAsync(User owner);
    public Task<IEnumerable<Test>> GetByPrivacyAsync(bool isPublic);
}