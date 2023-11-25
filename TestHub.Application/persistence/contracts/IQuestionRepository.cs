using Domain.entities;

namespace Application.persistence.contracts;

public interface IQuestionRepository : IGenericRepository<Question>
{
    Task<Question> GetQuestionWithDetails(int id);
    Task<List<Question>> GetQuestionsWithDetails();
}