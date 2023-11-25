using Application.dtos.respondsDto;
using Application.features.question.requests.queries;
using Application.persistence.contracts;

namespace Application.features.question.handlers.queries;

public class GetQuestionDetailedDtoRequestHandler : IRequestHandler<GetQuestionDtoRequest, RespondQuestionDto>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public GetQuestionDetailedDtoRequestHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<RespondQuestionDto> Handle(GetQuestionDtoRequest request, CancellationToken cancellationToken)
    {
        var question = await _repository.GetQuestionWithDetails(request.Id);
        
        return _mapper.Map<RespondQuestionDto>(question);
    }
}