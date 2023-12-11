using Application.dtos.respondsDto;
using Application.features.answer.requests.commands;
using Application.features.answer.requests.queries;
using TestHub.API.responces;

namespace TestHub.API.controllers;

[Route("api/Answer")]
[Produces("application/json")]
[ApiController]
public class AnswerController : Controller
{
    private readonly IMediator _mediator;

    public AnswerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RespondAnswerDto>>> Get()
    {
        var result = await _mediator.Send(new GetAnswerDtoListRequest());
        return BaseCommandResponse<List<RespondAnswerDto>>.GetBaseCommandResponseMessage(result);
    }

    [HttpGet("{id:int}", Name = "GetAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RespondAnswerDto>> GetAnswer(int id)
    {
        var command = new GetAnswerDtoRequest { Id = id };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondAnswerDto>.GetBaseCommandResponseMessage(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RespondAnswerDto>> CreateAnswer([FromBody] RequestAnswerDto? answerDto)
    {
        var command = new CreateAnswerCommand { AnswerDto = answerDto };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondAnswerDto>.GetBaseCommandResponseMessage(response);
    }

    [HttpDelete("{id:int}", Name = "DeleteAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondAnswerDto>> Delete(int id)
    {
        var command = new DeleteAnswerCommand { Id = id };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondAnswerDto>.GetBaseCommandResponseMessage(response);
    }

    [HttpPut("{id:int}", Name = "UpdateAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondAnswerDto>> UpdateAnswer(int id, RequestAnswerDto? answerDto)
    {
        var command = new UpdateAnswerCommand { Id = id, AnswerDto = answerDto };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondAnswerDto>.GetBaseCommandResponseMessage(response);
    }
}