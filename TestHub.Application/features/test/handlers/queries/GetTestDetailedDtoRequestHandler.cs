using Application.dtos;
using Application.dtos.respondsDto;
using Application.features.test.requests.queries;
using Application.persistence.contracts;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedDtoRequestHandler : IRequestHandler<GetTestDetailedDtoRequest, RespondTestDto>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedDtoRequestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RespondTestDto> Handle(GetTestDetailedDtoRequest request, CancellationToken cancellationToken)
    {
        var test = await _repository.GetTestWithDetails(request.Id);
        
        return _mapper.Map<RespondTestDto>(test);
    }
}