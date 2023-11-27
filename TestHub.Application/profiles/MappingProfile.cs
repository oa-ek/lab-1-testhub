using Application.dtos.sharedDTOs;

namespace Application.profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}