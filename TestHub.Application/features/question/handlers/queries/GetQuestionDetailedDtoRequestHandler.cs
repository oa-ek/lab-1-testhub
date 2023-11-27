using Application.features.question.requests.queries;

namespace Application.features.question.handlers.queries;

public class GetQuestionDetailedDtoRequestHandler : IRequestHandler<GetQuestionDetailedDtoRequest, BaseCommandResponse<RespondQuestionDto>>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public GetQuestionDetailedDtoRequestHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondQuestionDto>> Handle(GetQuestionDetailedDtoRequest request, CancellationToken cancellationToken)
    {
        var question = await _repository.GetQuestionWithDetails(request.Id);
        if (question == null) return new NotFoundFailedStatusResponse<RespondQuestionDto>(request.Id);
        
        var respondQuestionDto = _mapper.Map<RespondQuestionDto>(question);
        return new OkSuccessStatusResponse<RespondQuestionDto>(respondQuestionDto);
    }
}