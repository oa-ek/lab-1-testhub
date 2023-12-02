namespace Application.contracts.persistence;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<Category>> GetByTestId(int testId);
}