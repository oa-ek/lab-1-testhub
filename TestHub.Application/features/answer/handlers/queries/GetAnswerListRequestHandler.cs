using Application.dtos;
using Application.dtos.requestsDto;
using Application.features.answer.requests.queries;
using Application.persistence.contracts;

namespace Application.features.answer.handlers.queries;

public class GetAnswerListRequestHandler : IRequestHandler<GetAnswerListRequest, List<RequestAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerListRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RequestAnswerDto>> Handle(GetAnswerListRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll();

        return _mapper.Map<List<RequestAnswerDto>>(categories);
    }
}