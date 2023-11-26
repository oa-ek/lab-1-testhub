using Application.dtos.requestsDto;
using Application.features.test.requests.queries;
using Application.persistence.contracts;
using Domain.entities;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedDtoRequestHandler : IRequestHandler<GetTestDetailedDtoRequest, RequestTestDto>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedDtoRequestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RequestTestDto> Handle(GetTestDetailedDtoRequest request, CancellationToken cancellationToken)
    {
        var test = await _repository.GetTestWithDetails(request.Id);
        
        return _mapper.Map<RequestTestDto>(test);
    }
}