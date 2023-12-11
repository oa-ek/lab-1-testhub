using Application.features.answer.requests.queries;
using Application.results.common;

namespace Application.features.answer.handlers.queries;

public class GetAnswerDtoListRequestHandler : IRequestHandler<GetAnswerDtoListRequest, BaseCommandResult<List<RespondAnswerDto>>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerDtoListRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<List<RespondAnswerDto>>> Handle(GetAnswerDtoListRequest request, CancellationToken cancellationToken)
    {
        var answers = await _repository.GetAll();

        var respondAnswerDtos = _mapper.Map<List<RespondAnswerDto>>(answers);
        return new OkSuccessStatusResult<List<RespondAnswerDto>>(respondAnswerDtos);
    }
}