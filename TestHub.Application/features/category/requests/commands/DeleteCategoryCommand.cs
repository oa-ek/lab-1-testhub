namespace Application.features.category.requests.commands;

public class DeleteCategoryCommand : IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
}