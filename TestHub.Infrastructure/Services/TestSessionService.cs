using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;

namespace TestHub.Infrastructure.Services;

public class TestSessionService
{
    private readonly GenericRepository<TestSession> _testSessionRepository;
    private readonly GenericRepository<Certificate> _certificateRepository;

    public TestSessionService(GenericRepository<TestSession> testSessionRepository, GenericRepository<Certificate> certificateRepository)
    {
        _testSessionRepository = testSessionRepository;
        _certificateRepository = certificateRepository;
    }
    
    public IEnumerable<TestSession> GetAll()
    {
        return _testSessionRepository.Get(null, null, "User,Test");
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