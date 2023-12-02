using Application.contracts.persistence;

namespace TestHub.Persistence.repositories;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
    private readonly TestHubDbContext _context;
    
    public QuestionRepository(TestHubDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Question?> GetQuestionWithDetails(int id)
    {
        var question = await _context.Questions!
            .Include(q => q.Answers)
            .Include(q=>q.Type)
            .FirstOrDefaultAsync(q => q.Id == id);
        return question;
    }

    public async Task<List<Question>> GetQuestionsWithDetails()
    {
        var questions = await _context.Questions!
            .Include(q => q.Answers)
            .Include(q=>q.Type)
            .ToListAsync();
        return questions;
    }

    public async Task<List<Question>> GetQuestionsWithDetailsByTest(int testId)
    {
        var questions = await _context.Questions!
            .Where(q => q.TestId == testId)
            .Include(q => q.Answers)
            .Include(q=>q.Type)
            .ToListAsync();

        return questions;
    }
}