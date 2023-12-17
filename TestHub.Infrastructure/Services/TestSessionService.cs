using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class TestSessionService
{

    private readonly GenericRepository<TestSession> _testSessionRepository;

    public TestSessionService(GenericRepository<TestSession> testSessionRepository)
    {
        _testSessionRepository = testSessionRepository;
    }

    public IEnumerable<TestSession> GetAll()
    {
        return _testSessionRepository.Get();
    }

    public TestSession GetById(int id)
    {
        return _testSessionRepository.GetByID(id);
    }

    public void Add(TestSession testSession)
    {
        _testSessionRepository.Insert(testSession);
    }

    public void Delete(TestSession testSession)
    {
        _testSessionRepository.Delete(testSession);
    }
    //
    // public void Update(TestSession categoryToUpdate, CategoryDto categoryChanging)
    // {
    //     categoryToUpdate.Title = categoryChanging.Title;
    //     _testSessionRepository.Update(categoryToUpdate);
    // }
}
