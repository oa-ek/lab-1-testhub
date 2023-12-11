using Application.dtos.sharedDTOs;
using Application.results.common;

namespace Application.features.category.requests.commands;

public class CreateCategoryCommand : IRequest<BaseCommandResult<CategoryDto>>
{
    public required CategoryDto? CategoryDto { get; set; }
}