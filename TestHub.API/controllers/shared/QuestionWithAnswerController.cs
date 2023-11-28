using Application.dtos.respondsDto;
using Application.responses.failed;
using Application.responses.success;
using AutoMapper;
using TestHub.API.services;

namespace TestHub.API.controllers.shared;

[Route("api/QuestionWithAnswer")]
[Produces("application/json")]
[ApiController]
public class QuestionWithAnswerController
{
    private readonly QuestionService _questionService;
    
    public QuestionWithAnswerController(IMediator mediator, IMapper mapper)
    {
        _questionService = new QuestionService(mediator, mapper);
    }
    
    [HttpPost("{testId:int}", Name = "CreateQuestionWithAnswer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<BaseCommandResponse<RespondQuestionDto>> CreateQuestionWithAnswer(int testId, [FromBody] RequestQuestionWithAnswerDto? requestQuestionWithAnswerDto)
    {
        var createQuestionResponse = await _questionService.CreateQuestion(testId, requestQuestionWithAnswerDto);

        if (!createQuestionResponse.Success)
            return createQuestionResponse;

        var createdQuestionId = createQuestionResponse.ResponseObjectId!.Value;

        if (requestQuestionWithAnswerDto == null)
            return new CreatedSuccessStatusResponse<RespondQuestionDto>(createdQuestionId);
       
        var createAnswerResponse = await _questionService.CreateAnswers(createdQuestionId, requestQuestionWithAnswerDto.AnswerDtos);

        if (!createAnswerResponse.Success)
            return new BadRequestFailedStatusResponse<RespondQuestionDto>() { Errors = createAnswerResponse.Errors };

        return new CreatedSuccessStatusResponse<RespondQuestionDto>(createdQuestionId);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteQuestionWithAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<RespondQuestionDto>> DeleteQuestionWithAnswer(int id)
    {
        var deleteCommandResponse = await _questionService.DeleteQuestion(id);

        if (!deleteCommandResponse.Success)
            return deleteCommandResponse;
       
        var deleteAnswerResponse = await _questionService.DeleteQuestion(id);

        if (!deleteAnswerResponse.Success)
            return new BadRequestFailedStatusResponse<RespondQuestionDto>() { Errors = deleteAnswerResponse.Errors };

        return new OkSuccessStatusResponse<RespondQuestionDto>(id);
    }
    
    [HttpPut("{questionId:int}", Name = "UpdateQuestionAndAnswer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<BaseCommandResponse<RespondQuestionDto>> UpdateQuestionAnswer(int questionId, [FromBody] RequestQuestionWithAnswerDto? requestQuestionWithAnswerDto)
    {
        var createQuestionResponse = await _questionService.UpdateQuestion(questionId, requestQuestionWithAnswerDto);

        if (!createQuestionResponse.Success)
            return createQuestionResponse;

        var updatedQuestionId = createQuestionResponse.ResponseObjectId!.Value;

        if (requestQuestionWithAnswerDto == null)
            return new CreatedSuccessStatusResponse<RespondQuestionDto>(updatedQuestionId);
       
        var updateAnswerResponse = await _questionService.UpdateAnswers(updatedQuestionId, requestQuestionWithAnswerDto.AnswerDtos);

        if (!updateAnswerResponse.Success)
            return new BadRequestFailedStatusResponse<RespondQuestionDto>() { Errors = updateAnswerResponse.Errors };

        return new CreatedSuccessStatusResponse<RespondQuestionDto>(updatedQuestionId);
    }
}