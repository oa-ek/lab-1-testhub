using Application.dtos.respondsDto;
using Application.features.shared.questionwithanswer.requests.commands;
using Application.results.common;

namespace TestHub.API.controllers.shared;

[Route("api/QuestionWithAnswer")]
[Produces("application/json")]
[ApiController]
public class QuestionWithAnswerController
{
    private readonly IMediator _mediator;
    
    public QuestionWithAnswerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("{testId:int}", Name = "CreateQuestionWithAnswer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<BaseCommandResult<RespondQuestionDto>> CreateQuestionWithAnswer(int testId, [FromBody] RequestQuestionWithAnswerDto? requestQuestionWithAnswerDto)
    {
        var command = new CreateQuestionWithAnswersCommand { QuestionWithAnswerDto = requestQuestionWithAnswerDto, TestId = testId };
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpDelete("{id:int}", Name = "DeleteQuestionWithAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResult<RespondQuestionDto>> DeleteQuestionWithAnswer(int id)
    {
        var command = new DeleteQuestionWithAnswersCommand { QuestionId = id};
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpPut("{questionId:int}", Name = "UpdateQuestionAndAnswer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<BaseCommandResult<RespondQuestionDto>> UpdateQuestionAnswer(int questionId, [FromBody] RequestQuestionWithAnswerDto? requestQuestionWithAnswerDto)
    {
        var command = new UpdateQuestionWithAnswersCommand { QuestionWithAnswerDto = requestQuestionWithAnswerDto, QuestionId = questionId };
        var response = await _mediator.Send(command);
        return response;
    }
}