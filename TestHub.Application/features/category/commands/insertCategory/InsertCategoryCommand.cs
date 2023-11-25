namespace Application.features.category.commands.insertCategory;

public abstract record InsertCategoryCommand(CategoryDto CategoryDto) : IRequest<int>;
