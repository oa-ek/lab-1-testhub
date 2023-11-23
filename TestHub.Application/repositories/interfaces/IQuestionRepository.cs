using Domain.entities;

namespace Application.repositories.interfaces;

public interface IQuestionRepository : IBaseRepository<Category>
{
    public Task<IEnumerable<Question>> GetByTestAsync(Test test);
}