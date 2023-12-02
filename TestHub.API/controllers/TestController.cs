using Application.dtos.respondsDto;
using Application.features.test.requests.commands;
using Application.features.test.requests.queries;

namespace TestHub.API.controllers;

[Route("api/Test")]
[Produces("application/json")]
[ApiController]
public class TestController : Controller
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<RespondTestDto>?> Get()
    {
        var response = await _mediator.Send(new GetTestDetailedListRequest());
        return response.ResponseObject;
    }
    
    [HttpGet("{id:int}", Name = "GetTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<RespondTestDto?> GetTest(int id)
    {
        var response = await _mediator.Send(new GetTestDetailedDtoRequest { Id = id });
        return response.ResponseObject;
    }
    
    [HttpGet("GetByUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<RespondTestDto>?> GetTestDtosByUser()
    {
        var ownerId = 2;
        var command = new GetTestDetailedListRequestByUser { OwnerId = ownerId };
        var response = await _mediator.Send(command);
        return response.ResponseObject;
    }
    
    [HttpGet("GetPublicTests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<RespondTestDto>?> GetPublicTests()
    {
        var command = new GetTestDetailedListRequestByPublicity( );
        var response = await _mediator.Send(command);
        return response.ResponseObject;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<BaseCommandResponse<RespondTestDto>> CreateTest([FromBody] RequestTestDto? testDto)
    {
        var ownerId = 2;
        var command = new CreateTestCommand() { TestDto = testDto, OwnerId = ownerId};
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpDelete("{id:int}", Name = "DeleteTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<RespondTestDto>> Delete(int id)
    {
        var command = new DeleteTestCommand() { Id = id };
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpPut("{id:int}", Name = "UpdateTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<RespondTestDto>> UpdateTest(int id, RequestTestDto? testDto)
    {
        var command = new UpdateTestCommand() { Id = id, TestDto = testDto };
        var response = await _mediator.Send(command);
        return response;
    }
}