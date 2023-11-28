using Application.contracts.persistence;

namespace TestHub.Persistence.repositories;

public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
{
    private readonly TestHubDbContext _context;
    
    public AnswerRepository(TestHubDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Answer>> GetByQuestion(int questionId)
    {
        var answers = await _context.Answers!
            .Where(q => q.QuestionId == questionId)
            .ToListAsync();

        return answers;
    }
}