using Application.dtos.sharedDTOs;
using Application.results.common;

namespace Application.features.category.requests.commands;

public class DeleteCategoryCommand : IRequest<BaseCommandResult<CategoryDto>>
{
    public int Id { get; set; }
}