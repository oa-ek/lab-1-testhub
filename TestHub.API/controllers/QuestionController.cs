using Application.dtos.respondsDto;
using Application.features.question.requests.commands;
using Application.features.question.requests.queries;
using TestHub.API.responces;

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
    public async Task<ActionResult<List<RespondQuestionDto>>> Get()
    {
        var response = await _mediator.Send(new GetQuestionDetailedListRequest());
        return BaseCommandResponse<List<RespondQuestionDto>>.GetBaseCommandResponseMessage(response);
    }
    
    [HttpGet("{id:int}", Name = "GetQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondQuestionDto>> GetQuestion(int id)
    {
        var response = await _mediator.Send(new GetQuestionDetailedDtoRequest() { Id = id });
        return BaseCommandResponse<RespondQuestionDto>.GetBaseCommandResponseMessage(response);
    }
    
    [HttpGet("getByTest/{testId:int}",  Name = "GetQuestionListByTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RespondQuestionDto>>> GetAllQuestionByTest(int testId)
    {
        var command = new GetQuestionDetailedListRequestByTest{ TestId = testId};
        var response = await _mediator.Send(command);
        return BaseCommandResponse<List<RespondQuestionDto>>.GetBaseCommandResponseMessage(response);
    }
    
    [HttpPost(Name = "CreateQuestion")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RespondQuestionDto>> CreateQuestion([FromBody] RequestQuestionDto? questionDto, int testId)
    {
        var command = new CreateQuestionCommand() { QuestionDto = questionDto, TestId = testId };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondQuestionDto>.GetBaseCommandResponseMessage(response);
    }
    
    [HttpPut("{id:int}", Name = "UpdateQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondQuestionDto>> UpdateQuestion(int id, RequestQuestionDto? questionDto)
    {
        var command = new UpdateQuestionCommand() { Id = id, QuestionDto = questionDto };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondQuestionDto>.GetBaseCommandResponseMessage(response);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondQuestionDto>> DeleteQuestion(int id)
    {
        var command = new DeleteQuestionCommand() { Id = id };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondQuestionDto>.GetBaseCommandResponseMessage(response);
    }
}