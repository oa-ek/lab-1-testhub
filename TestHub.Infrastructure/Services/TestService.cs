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
        testToUpdate.Description = testChanging.Description;
        testToUpdate.Duration = testChanging.Duration;
        testToUpdate.IsPublic = testChanging.IsPublic;
        testToUpdate.UpdatedAt = DateTime.Now;
        
        _testRepository.Update(testToUpdate);
    }

    public void UpdateCategories(Test testToUpdate, string[] testDtoCategories)
    {
        var categories = _categoryService.GetAll();
        var existingCategories = _testCategoryRepository.Get()
            .Where(tc => tc.TestId == testToUpdate.Id)
            .ToList();
        
        foreach (var existingCategory in existingCategories)
            _testCategoryRepository.Delete(existingCategory);
        
        foreach (var category in testDtoCategories)
        {
            var testCategory = new TestCategory
            {
                TestId = testToUpdate.Id,
                Category= categories.FirstOrDefault(c=>c.Title==category)
            };
            _testCategoryRepository.Insert(testCategory);
        }
    }
    
    public Category[] GetCategoriesByTestId(int testId)
    {
        var testCategories = _testCategoryRepository.Get().Where(tc => tc.TestId == testId);
        var categories = testCategories.Select(tc => new Category
        {
            Id = tc.CategoryId,
            Title = _categoryService.GetById(tc.CategoryId).Title
        }).ToArray();

        return categories;
    }

}
