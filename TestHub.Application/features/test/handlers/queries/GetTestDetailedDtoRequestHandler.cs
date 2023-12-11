using Application.features.test.requests.queries;
using Application.results.common;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedDtoRequestHandler : IRequestHandler<GetTestDetailedDtoRequest, BaseCommandResult<RespondTestDto>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedDtoRequestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<RespondTestDto>> Handle(GetTestDetailedDtoRequest request, CancellationToken cancellationToken)
    {
        var test = await _repository.GetTestWithDetails(request.Id);
        if (test == null) return new NotFoundFailedStatusResult<RespondTestDto>(request.Id);

        var respondTestDto = _mapper.Map<RespondTestDto>(test);
        return new OkSuccessStatusResult<RespondTestDto>(respondTestDto);
    }
}