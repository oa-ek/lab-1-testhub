namespace Application.contracts.persistence;

public interface IAnswerRepository : IGenericRepository<Answer>
{
    Task<List<Answer>> GetByQuestion(int questionId);
}