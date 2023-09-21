using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class CategoryService
{
    private readonly GenericRepository<Category> _categoryRepository;

    public CategoryService(GenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> GetAll()
    {
        return _categoryRepository.Get();
    }

    public Category GetById(int id)
    {
        return _categoryRepository.GetByID(id);
    }

    public void Add(Category category)
    {
        _categoryRepository.Insert(category);
    }

    public void Delete(Category categoryDtoToDelete)
    {
        _categoryRepository.Delete(categoryDtoToDelete);
    }

    public void Update(Category categoryToUpdate, CategoryDto categoryChanging)
    {
        categoryToUpdate.Title = categoryChanging.Title;
        _categoryRepository.Update(categoryToUpdate);
    }
}