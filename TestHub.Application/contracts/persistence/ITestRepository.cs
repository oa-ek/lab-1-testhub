namespace Application.contracts.persistence;

public interface ITestRepository : IGenericRepository<Test>
{
    Task<Test?> GetTestWithDetails(int id);
    Task<List<Test>> GetTestWithDetails();
    Task<List<Test>> GetTestWithDetailsByUser(int ownerId);
    Task<List<Test>> GetTestWithDetailsByPublicity();
    Task SetCategories(Test test, CategoryDto categoryDto);
    Task DeleteCategories(Test test);
}