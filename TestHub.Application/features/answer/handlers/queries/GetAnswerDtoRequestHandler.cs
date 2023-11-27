using Application.features.answer.requests.queries;

namespace Application.features.answer.handlers.queries;

public class GetAnswerDtoRequestHandler : IRequestHandler<GetAnswerDtoRequest, BaseCommandResponse<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerDtoRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondAnswerDto>> Handle(GetAnswerDtoRequest request, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(request.Id);
        if (answer == null) return new NotFoundFailedStatusResponse<RespondAnswerDto>(request.Id);
    
        var answerDto = _mapper.Map<RespondAnswerDto>(answer);
        return new OkSuccessStatusResponse<RespondAnswerDto>(answerDto);
    }
}