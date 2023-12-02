﻿using Application.features.test.requests.commands;

namespace Application.features.test.handlers.commands;

public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, BaseCommandResponse<RespondTestDto>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTestCommandHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondTestDto>> Handle(UpdateTestCommand command, CancellationToken cancellationToken)
    {
        if (command.TestDto == null)
            return new BadRequestFailedStatusResponse<RespondTestDto>(new List<ValidationFailure>
            {
                new ("TestDto", "TestDto cannot be null.")
            });
        
        var validator = new RequestTestDtoValidator();
        var validationResult = await validator.ValidateAsync(command.TestDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondTestDto>(validationResult.Errors);
        
        var test = await _repository.Get(command.Id);
        if (test == null) return new NotFoundFailedStatusResponse<RespondTestDto>(command.Id);
        
        _mapper.Map(command.TestDto, test);
        await _repository.Update(test);

        await _repository.DeleteCategories(test);
        await ProcessCategoriesAsync(test, command.TestDto.Categories);
        return new OkSuccessStatusResponse<RespondTestDto>(test.Id);
    }
    
    private async Task ProcessCategoriesAsync(Test test, IEnumerable<CategoryDto> requestCategories)
    {
        foreach (var categoryDto in requestCategories)
        {
            await _repository.SetCategories(test, categoryDto);
        }
    }
}