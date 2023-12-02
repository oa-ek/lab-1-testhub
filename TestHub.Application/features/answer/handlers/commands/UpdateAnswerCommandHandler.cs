using Application.features.answer.requests.commands;

namespace Application.features.answer.handlers.commands;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, BaseCommandResponse<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public UpdateAnswerCommandHandler(IAnswerRepository repository, IMapper mapper, IFileService fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<BaseCommandResponse<RespondAnswerDto>> Handle(UpdateAnswerCommand command,
        CancellationToken cancellationToken)
    {
        if (command.AnswerDto == null)
            return new BadRequestFailedStatusResponse<RespondAnswerDto>(new List<ValidationFailure>
            {
                new("AnswerDto", "AnswerDto cannot be null.")
            });

        var validator = new RequestAnswerDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.AnswerDto, cancellationToken);
        if (!validationResult.IsValid)
            return new BadRequestFailedStatusResponse<RespondAnswerDto>(validationResult.Errors);

        var answer = await _repository.Get(command.Id);
        if (answer == null) return new NotFoundFailedStatusResponse<RespondAnswerDto>(command.Id);

        _mapper.Map(command.AnswerDto, answer);
        answer.ImageUrl = await MapImageAsync(command.AnswerDto.Image);
        
        await _repository.Update(answer);
        return new OkSuccessStatusResponse<RespondAnswerDto>(answer.Id);
    }

    private async Task<string> MapImageAsync(FileDto? imageDto)
    {
        return imageDto == null ? string.Empty : await _fileService.UploadImage(imageDto);
    }
}