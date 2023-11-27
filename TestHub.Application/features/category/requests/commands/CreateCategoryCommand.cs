using Application.dtos.sharedDTOs;

namespace Application.features.category.requests.commands;

public class CreateCategoryCommand : IRequest<BaseCommandResponse<CategoryDto>>
{
    public required CategoryDto? CategoryDto { get; set; }
}