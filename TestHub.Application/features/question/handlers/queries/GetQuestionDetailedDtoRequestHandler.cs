using Application.dtos;
using Application.features.question.requests.queries;
using Application.persistence.contracts;

namespace Application.features.question.handlers.queries;

public class GetQuestionDetailedDtoRequestHandler : IRequestHandler<GetQuestionDtoRequest, QuestionDto>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public GetQuestionDetailedDtoRequestHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<QuestionDto> Handle(GetQuestionDtoRequest request, CancellationToken cancellationToken)
    {
        var question = await _repository.GetQuestionWithDetails(request.Id);
        
        return _mapper.Map<QuestionDto>(question);
    }
}