using Application.persistence.dtos;

namespace Application.features.category.requests.commands;

public class UpdateCategoryCommand: IRequest<Unit>
{
    public int Id { get; set; }
    public required CategoryDto CategoryDto { get; set; }
}