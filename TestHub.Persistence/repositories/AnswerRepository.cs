namespace TestHub.Persistence.repositories;

public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
{
    private readonly TestHubDbContext _context;
    
    public AnswerRepository(TestHubDbContext context) : base(context)
    {
        _context = context;
    }
}