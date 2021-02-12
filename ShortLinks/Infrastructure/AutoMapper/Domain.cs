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
                .ForMember(dest => dest.ExpirationDate,
                    opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.OriginalLink,
                    opt => opt.MapFrom(src => src.OriginalLink));
            CreateMap<Link, OutputLinkDTO>()
                .ForMember(dest => dest.ShortLink,
                    opt => opt.MapFrom(src => src.ShortLink))
                .ForMember(dest => dest.ExpirationDate,
                    opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.OriginalLink,
                    opt => opt.MapFrom(src => src.OriginalLink));
            CreateMap<OutputLinkDTO, Link>()
                .ForMember(dest => dest.OriginalLink,
                    opt => opt.MapFrom(src => src.OriginalLink))
                .ForMember(dest => dest.ShortLink,
                    opt => opt.MapFrom(src => src.ShortLink))
                .ForMember(dest => dest.ExpirationDate,
                    opt => opt.MapFrom(src => src.ExpirationDate));
            //CreateMap<InputLinkPutDTO, Link>()
            //    .ForMember(dest => dest.ExpirationDate,
            //        opt => opt.MapFrom(src => src.ExpirationDate))
            //    .ForMember(dest => dest.OriginalLink,
            //        opt => opt.MapFrom(src => src.MutableOriginalLink));
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
