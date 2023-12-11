using Application.contracts.infrastructure.file;
using Application.features.answer.requests.commands;
using Application.results.common;

namespace Application.features.answer.handlers.commands;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, BaseCommandResult<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CreateAnswerCommandHandler(IAnswerRepository repository, IMapper mapper, IFileService fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;
    }
    
    public async Task<BaseCommandResult<RespondAnswerDto>> Handle(CreateAnswerCommand command, CancellationToken cancellationToken)
    {
        if (command.AnswerDto == null)
            return new BadRequestFailedStatusResult<RespondAnswerDto>(new List<ValidationFailure>
            {
                new ("AnswerDto", "AnswerDto cannot be null.")
            });
        
        var validator = new RequestAnswerDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.AnswerDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResult<RespondAnswerDto>(validationResult.Errors);
        
        var answer = _mapper.Map<Answer>(command.AnswerDto);
        answer.QuestionId = command.QuestionId;
        answer.ImageUrl = await MapImageAsync(command.AnswerDto.Image);

        answer = await _repository.Add(answer);
        return new CreatedSuccessStatusResult<RespondAnswerDto>(answer.Id);
    }
    
    private async Task<string> MapImageAsync(FileDto? imageDto)
    {
        return imageDto == null ? string.Empty : await _fileService.UploadImage(imageDto);
    }
}