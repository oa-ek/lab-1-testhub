using Application.dtos.sharedDTOs;
using Application.results.common;

namespace Application.features.category.requests.commands;

public class UpdateCategoryCommand: IRequest<BaseCommandResult<CategoryDto>>
{
    public int Id { get; set; }
    public required CategoryDto? CategoryDto { get; set; }
}