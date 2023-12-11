using Application.contracts.infrastructure.file;
using Application.features.question.requests.commands;
using Application.results.common;

namespace Application.features.question.handlers.commands;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, BaseCommandResult<RespondQuestionDto>>
{
    private readonly IQuestionRepository _repository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public UpdateQuestionCommandHandler(IQuestionRepository repository, IMapper mapper, IFileService fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;
    }
    public async Task<BaseCommandResult<RespondQuestionDto>> Handle(UpdateQuestionCommand command, CancellationToken cancellationToken)
    {
        if (command.QuestionDto == null)
            return new BadRequestFailedStatusResult<RespondQuestionDto>(new List<ValidationFailure>
            {
                new ("QuestionDto", "QuestionDto cannot be null.")
            });
        
        var validator = new RequestQuestionDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.QuestionDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResult<RespondQuestionDto>(validationResult.Errors);
        
        var question = await _repository.Get(command.Id);
        if (question == null) return new NotFoundFailedStatusResult<RespondQuestionDto>(command.Id);
        
        _mapper.Map(command.QuestionDto, question);
        question.ImageUrl = await MapImageAsync(command.QuestionDto.Image);
        
        await _repository.Update(question);
        return new OkSuccessStatusResult<RespondQuestionDto>(question.Id);
    }
    
    private async Task<string> MapImageAsync(FileDto? imageDto)
    {
        return imageDto == null ? string.Empty : await _fileService.UploadImage(imageDto);
    }
}