using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class TestService
{

    private readonly GenericRepository<Test> _testRepository;
    private readonly GenericRepository<TestCategory> _testCategoryRepository;
    private readonly CategoryService _categoryService;

    public TestService(GenericRepository<Test> testRepository, 
        GenericRepository<TestCategory> testCategoryRepository, 
        CategoryService categoryService)
    {
        _testRepository = testRepository;
        _testCategoryRepository = testCategoryRepository;
        _categoryService = categoryService;
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
    
    public void SetCategories(Test test, string[] categories)
    {
        foreach (var category in categories)
        {
            var testCategory = new TestCategory
            {
                Test = test,
                Category = _categoryService.GetById(int.Parse(category))
            };
            _testCategoryRepository.Insert(testCategory);
        }
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
