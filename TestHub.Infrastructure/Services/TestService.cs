using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class TestService
{

    private readonly GenericRepository<Test> _testRepository;

    public TestService(GenericRepository<Test> testRepository)
    {
        _testRepository = testRepository;
    }

    public IEnumerable<Test> GetAll()
    {
        return _testRepository.Get();
    }

    public Test GetById(int id)
    {
        return _testRepository.GetByID(id);
    }

    public void Add(Test test)
    {
        _testRepository.Insert(test);
    }

    public void Delete(Test testDtoToDelete)
    {
        _testRepository.Delete(testDtoToDelete);
    }

    public void Update(Test testToUpdate, TestDto testChanging)
    {
        testToUpdate.Title = testChanging.Title;
        _testRepository.Update(testToUpdate);
    }
}
