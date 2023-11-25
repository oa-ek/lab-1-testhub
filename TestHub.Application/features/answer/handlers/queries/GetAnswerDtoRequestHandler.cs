using Application.dtos;
using Application.features.answer.requests.queries;
using Application.persistence.contracts;

namespace Application.features.answer.handlers.queries;

public class GetAnswerDtoRequestHandler : IRequestHandler<GetAnswerDtoRequest, AnswerDto>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public GetAnswerDtoRequestHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<AnswerDto> Handle(GetAnswerDtoRequest request, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(request.Id);
        
        return _mapper.Map<AnswerDto>(answer);
    }
}