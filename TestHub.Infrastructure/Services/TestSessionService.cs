using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class TestSessionService
{

    private readonly GenericRepository<TestSession> _testSessionRepository;
    private readonly GenericRepository<StatusSessionQuestion> _statusSessionQuestion;

    public TestSessionService(GenericRepository<TestSession> testSessionRepository, 
        GenericRepository<StatusSessionQuestion> statusSessionQuestion)
    {
        _testSessionRepository = testSessionRepository;
        _statusSessionQuestion = statusSessionQuestion;
    }

    public IEnumerable<TestSession> GetAll()
    {
        return _testSessionRepository.Get();
    }

    public TestSession GetById(int id)
    {
        return _testSessionRepository.GetByID(id);
    }

    public void Add(TestSession testSession)
    {
        _testSessionRepository.Insert(testSession);
    }

    public void Delete(TestSession testSession)
    {
        _testSessionRepository.Delete(testSession);
    }
    
    public void Update(TestSession testSesssionUpdate)
    {
        _testSessionRepository.Update(testSesssionUpdate);
    }
    
    
    public void AddStatus(StatusSessionQuestion statusSessionQuestion)
    {
        _statusSessionQuestion.Insert(statusSessionQuestion);
    }

    public IEnumerable<StatusSessionQuestion> GetAllStatuses()
    {
        return _statusSessionQuestion.Get();
    }

    public void UpdateStatus(StatusSessionQuestion statusSessionQuestion, StatusQuestionSessionDto statusChanging)
    {
        statusSessionQuestion.Attepts += 1;
        statusSessionQuestion.IsCorrect = statusChanging.IsCorrect;
        _statusSessionQuestion.Update(statusSessionQuestion);
    }
    
    
}
