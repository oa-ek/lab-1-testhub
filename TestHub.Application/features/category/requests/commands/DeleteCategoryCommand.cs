using Application.persistence.dtos;

namespace Application.features.category.requests.commands;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}