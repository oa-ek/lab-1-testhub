using Domain.entities;

namespace Application.repositories.interfaces;

public interface IAnswerRepository : IBaseRepository<Answer>
{
    public Task<IEnumerable<Answer>> GetByQuestionAsync(Question question);
}