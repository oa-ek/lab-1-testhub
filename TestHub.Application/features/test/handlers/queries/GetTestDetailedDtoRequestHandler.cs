using Application.features.test.requests.queries;

namespace Application.features.test.handlers.queries;

public class GetTestDetailedDtoRequestHandler : IRequestHandler<GetTestDetailedDtoRequest, BaseCommandResponse<RespondTestDto>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetTestDetailedDtoRequestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<RespondTestDto>> Handle(GetTestDetailedDtoRequest request, CancellationToken cancellationToken)
    {
        var test = await _repository.GetTestWithDetails(request.Id);
        if (test == null) return new NotFoundFailedStatusResponse<RespondTestDto>(request.Id);

        var respondTestDto = _mapper.Map<RespondTestDto>(test);
        return new OkSuccessStatusResponse<RespondTestDto>(respondTestDto);
    }
}