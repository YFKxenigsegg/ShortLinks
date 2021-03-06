﻿using AutoMapper;
using ShortLinks.Models.DTO;
using ShortLinks.Models.Entities;

namespace ShortLinks.Infasctructure.AutoMapper
{
    public class LinkProfile : Profile
    {
        public LinkProfile()
        {
            CreateMap<InputLinkDTO, Link>();
            CreateMap<InputLinkCreateDTO, Link>();
            CreateMap<Link, OutputLinkDTO>();
            CreateMap<OutputLinkDTO, Link>();
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
