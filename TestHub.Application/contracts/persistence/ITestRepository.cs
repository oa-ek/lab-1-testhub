namespace Application.contracts.persistence;

public interface ITestRepository : IGenericRepository<Test>
{
    Task<Test?> GetTestWithDetails(int id);
    Task<List<Test>> GetTestWithDetails();
}