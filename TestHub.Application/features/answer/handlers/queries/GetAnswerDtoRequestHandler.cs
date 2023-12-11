using Application.features.answer.requests.queries;
using Application.results.common;

namespace Application.features.answer.handlers.queries;

public class GetAnswerDtoRequestHandler : IRequestHandler<GetAnswerDtoRequest, BaseCommandResult<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerDtoRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResult<RespondAnswerDto>> Handle(GetAnswerDtoRequest request, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(request.Id);
        if (answer == null) return new NotFoundFailedStatusResult<RespondAnswerDto>(request.Id);
    
        var answerDto = _mapper.Map<RespondAnswerDto>(answer);
        return new OkSuccessStatusResult<RespondAnswerDto>(answerDto);
    }
}