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
            .FirstOrDefaultAsync(q => q.Id == id);
        return question;
    }

    public async Task<List<Question>> GetQuestionsWithDetails()
    {
        var questions = await _context.Questions!
            .Include(q => q.Answers)
            .ToListAsync();
        return questions;
    }
}