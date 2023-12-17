using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers;

[Route("api/TestSession")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class TestSessionController : Controller
{
    private readonly TestSessionService _sessionService;
    private readonly UserService _userService;
    private readonly FileService _fileService;
    private readonly ILogger _logger;

    public TestSessionController(TestSessionService sessionService, ILogger<TestSessionController> logger, UserService userService, FileService fileService)
    {
        _sessionService = sessionService;
        _logger = logger;
        _userService = userService;
        _fileService = fileService;

        // Log the type being injected
        _logger.LogInformation($"Injected testSessionService of type: {sessionService.GetType()}");
        _logger.LogInformation($"Injected userService of type: {userService.GetType()}");
    }
    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<TestSession>> GetAll()
    {
        return Ok(_sessionService.GetAll());
    }
    
    [HttpGet("GetByUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<TestSession>> GetByUser()
    {
        return Ok(_sessionService.GetAll().Where(u => u.User == _userService.GerRegistrationUser()));
    }
    
    [HttpGet("Certificate/GetByUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Certificate>> GetCertificatesByUser()
    {
        return Ok(_sessionService.GetCertificates().Where(u => u.Owner == _userService.GerRegistrationUser()));
    }
    
    [HttpPost("{data}", Name = "Certificate/CreateByLevel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> CreateByLevel([FromBody] LevelData data)
    {
        var userName = _userService.GerRegistrationUser()!.Name;

        var certificates = _sessionService.GetCertificates();
        var hasLevel01Certificate = certificates.Any(certificate => certificate.Name.Contains(data.Level));
        if (hasLevel01Certificate)
            return BadRequest($"Already have certificate for this level.");
        
        var certificate = CertificateGenerator.GenerateCertificate(userName, data.Level);
        if (certificate == null)
            return BadRequest($"Failed creation certificate for {userName}.");
        
        var link = await _fileService.UploadImage(certificate);
        _logger.LogInformation($"Create certificate - {certificate.FileName} for {userName}.");
        
        _sessionService.Add(link, certificate, _userService.GerRegistrationUser()!);
        
        return Ok(link);
    }
}