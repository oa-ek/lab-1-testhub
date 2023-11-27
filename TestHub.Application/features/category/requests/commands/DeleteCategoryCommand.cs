using Application.dtos.sharedDTOs;

namespace Application.features.category.requests.commands;

public class DeleteCategoryCommand : IRequest<BaseCommandResponse<CategoryDto>>
{
    public int Id { get; set; }
}