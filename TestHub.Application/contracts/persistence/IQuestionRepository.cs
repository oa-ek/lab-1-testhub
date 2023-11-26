namespace Application.contracts.persistence;

public interface IQuestionRepository : IGenericRepository<Question>
{
    Task<Question?> GetQuestionWithDetails(int id);
    Task<List<Question>> GetQuestionsWithDetails();
}