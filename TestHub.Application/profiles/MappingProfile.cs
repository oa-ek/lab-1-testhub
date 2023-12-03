namespace Application.profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        
        CreateMap<Test, RequestTestDto>().ReverseMap();
        CreateMap<Test, RespondTestDto>().ReverseMap();
        
        CreateMap<RequestAnswerDto, Answer>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image));
        CreateMap<Answer, RespondAnswerDto>()
            .ForMember(dest => dest.Image, opt=> opt.MapFrom(src=>src.ImageUrl))
            .ReverseMap();

        CreateMap<Question, RespondQuestionDto>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
            .ReverseMap();
        CreateMap<RequestQuestionDto, Question>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image));
        
        CreateMap<RequestQuestionWithAnswerDto, RespondQuestionDto>();
    }
}