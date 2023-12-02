using Application.features.question.requests.commands;

namespace Application.features.question.handlers.commands;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, BaseCommandResponse<RespondQuestionDto>>
{
    private readonly IQuestionRepository _repository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CreateQuestionCommandHandler(IQuestionRepository repository, IMapper mapper, IFileService fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;
    }
    
    public async Task<BaseCommandResponse<RespondQuestionDto>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        if (command.QuestionDto == null)
            return new BadRequestFailedStatusResponse<RespondQuestionDto>(new List<ValidationFailure>
            {
                new ("QuestionDto", "QuestionDto cannot be null.")
            });
        
        var validator = new RequestQuestionDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.QuestionDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondQuestionDto>(validationResult.Errors);
        
        var question = _mapper.Map<Question>(command.QuestionDto);
        question.TestId = command.TestId;
        question.ImageUrl = await MapImageAsync(command.QuestionDto.Image);

        question = await _repository.Add(question);
        return new CreatedSuccessStatusResponse<RespondQuestionDto>(question.Id);
    }
    
    private async Task<string> MapImageAsync(FileDto? imageDto)
    {
        return imageDto == null ? string.Empty : await _fileService.UploadImage(imageDto);
    }
}