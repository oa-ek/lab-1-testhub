using Domain.entities;

namespace Application.persistence.contracts;

public interface ITestRepository : IGenericRepository<Test>
{
    Task<Test?> GetTestWithDetails(int id);
    Task<List<Test>> GetTestWithDetails();
}