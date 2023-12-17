using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class TestSessionService
{

    private readonly GenericRepository<TestSession> _testSessionRepository;
    private readonly GenericRepository<StatusSessionQuestion> _statusSessionQuestion;
    private readonly GenericRepository<Certificate> _certificateRepository;

    public TestSessionService(GenericRepository<TestSession> testSessionRepository, 
        GenericRepository<StatusSessionQuestion> statusSessionQuestion, GenericRepository<Certificate> certificateRepository)
    {
        _testSessionRepository = testSessionRepository;
        _statusSessionQuestion = statusSessionQuestion;
        _certificateRepository = certificateRepository;
    }
   


    public IEnumerable<TestSession> GetAll()
    {
        return _testSessionRepository.Get(null, null, "User,Test,StatusSessionQuestions");
    }


    public TestSession GetById(int id)
    {
        return _testSessionRepository.GetByID(id);
    }

    public void AddSession(TestSession testSession)
    {
        _testSessionRepository.Insert(testSession);
    }

    public void Delete(TestSession testSession)
    {
        _testSessionRepository.Delete(testSession);
    }
    
    public void Update(TestSession testSesssionUpdate)
    {
        Console.WriteLine(testSesssionUpdate);
        _testSessionRepository.Update(testSesssionUpdate);
    }
    
    
    public void AddStatus(StatusSessionQuestion statusSessionQuestion)
    {
        _statusSessionQuestion.Insert(statusSessionQuestion);
    }

    public IEnumerable<StatusSessionQuestion> GetAllStatusesSession(int session)
    {
        return _statusSessionQuestion.Get().Where(s=>s.SessionId==session);
    }

    public void UpdateStatus(StatusSessionQuestion statusSessionQuestion, StatusQuestionSessionDto statusChanging)
    {
        statusSessionQuestion.Attepts += 1;
        statusSessionQuestion.IsCorrect = statusChanging.IsCorrect;
        _statusSessionQuestion.Update(statusSessionQuestion);
    }
    
    public IEnumerable<StatusSessionQuestion> GetAllStatuses()
    {
        return _statusSessionQuestion.Get();
    }

   
    
    public IEnumerable<Certificate> GetCertificates()
    {
        return _certificateRepository.Get(null, null, "Owner");
    }
    
    public void Add(string link, FileDto file, User user)
    {
        var certificate = new Certificate
        {
            IssueDate = DateTime.Today,
            Name = file.FileName,
            CertificateNumber = Guid.NewGuid().ToString()[..8],
            ImageUrl = link,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Owner = user
        };
        
        _certificateRepository.Insert(certificate);
    }
}
