using Domain.entities;

namespace Application.features.category;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
    }
}