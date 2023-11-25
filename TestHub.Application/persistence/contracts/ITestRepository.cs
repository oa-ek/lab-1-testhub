using Domain.entities;

namespace Application.persistence.contracts;

public interface ITestRepository : IGenericRepository<Test>
{
    Task<Question> GetTestWithDetails(int id);
    Task<List<Question>> GetTestWithDetails();
}