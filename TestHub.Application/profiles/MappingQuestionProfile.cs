namespace Application.profiles;

public class MappingQuestionProfile : Profile
{
    public MappingQuestionProfile()
    {
        CreateMap<Question, RespondQuestionDto>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers)).ReverseMap();
        
        CreateMap<Question, RequestQuestionDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<RequestQuestionWithAnswerDto, RespondQuestionDto>().ReverseMap();
    }
}