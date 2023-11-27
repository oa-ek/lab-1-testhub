using Application.dtos.sharedDTOs;

namespace Application.features.category.requests.commands;

public class UpdateCategoryCommand: IRequest<BaseCommandResponse<CategoryDto>>
{
    public int Id { get; set; }
    public required CategoryDto? CategoryDto { get; set; }
}