using AutoMapper;
using ShortLinks.Models.DTO;
using ShortLinks.Models.Entities;

namespace ShortLinks.Infasctructure.AutoMapper
{
    public class LinkProfile : Profile
    {
        public LinkProfile()
        {
            CreateMap<InputLinkDTO, Link>()
                .ForMember(dest => dest.ShortLinkId,
                    opt => opt.MapFrom(src => src.Id));
            CreateMap<Link, OutputLinkDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.ShortLinkId));
        }
    }
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AuthUserDTO, User>()
                .ForMember(dest => dest.PasswordCode, 
                    opt => opt.MapFrom(src => src.Password));
        }
    }
}
