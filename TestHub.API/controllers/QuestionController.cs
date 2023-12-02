using Application.dtos.respondsDto;
using Application.features.question.requests.commands;
using Application.features.question.requests.queries;

namespace TestHub.API.controllers;

[Route("api/Question")]
[Produces("application/json")]
[ApiController]
public class QuestionController : Controller
{
    private readonly IMediator _mediator;

    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Name = "GetQuestionList")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<RespondQuestionDto>?> Get()
    {
        var response = await _mediator.Send(new GetQuestionDetailedListRequest());
        return response.ResponseObject;
    }
    
    [HttpGet("{id:int}", Name = "GetQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<RespondQuestionDto?> GetQuestion(int id)
    {
        var response = await _mediator.Send(new GetQuestionDetailedDtoRequest() { Id = id });
        return response.ResponseObject;
    }
    
    [HttpGet("getByTest/{testId:int}",  Name = "GetQuestionListByTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<RespondQuestionDto>?> GetAllQuestionByTest(int testId)
    {
        var command = new GetQuestionDetailedListRequestByTest{ TestId = testId};
        var response = await _mediator.Send(command);
        return response.ResponseObject;
    }
    
    [HttpPost(Name = "CreateQuestion")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<BaseCommandResponse<RespondQuestionDto>> CreateQuestion([FromBody] RequestQuestionDto? questionDto, int testId)
    {
        var command = new CreateQuestionCommand() { QuestionDto = questionDto, TestId = testId };
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpPut("{id:int}", Name = "UpdateQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<RespondQuestionDto>> UpdateQuestion(int id, RequestQuestionDto? questionDto)
    {
        var command = new UpdateQuestionCommand() { Id = id, QuestionDto = questionDto };
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpDelete("{id:int}", Name = "DeleteQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<RespondQuestionDto>> DeleteQuestion(int id)
    {
        var command = new DeleteQuestionCommand() { Id = id };
        var response = await _mediator.Send(command);
        return response;
    }
}