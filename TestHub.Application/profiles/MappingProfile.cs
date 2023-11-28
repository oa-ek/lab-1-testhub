namespace Application.profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        
        CreateMap<Test, RequestTestDto>().ReverseMap();
        CreateMap<Test, RespondTestDto>().ReverseMap();
    }
}