namespace Application.dtos.requestsDto;

public class RequestQuestionWithAnswerDto : RespondQuestionDto
{
    public RequestAnswerDto[]? AnswerDtos { get; set; }
}