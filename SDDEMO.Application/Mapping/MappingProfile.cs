using AutoMapper;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using SDDEMO.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginViewModel>().
               ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id)).
               ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username)).
               ForMember(dest => dest.mailAddress, opt => opt.MapFrom(src => src.mailAddress));

            CreateMap<User, RegisterViewModel>().
               ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id)).
               ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username)).
               ForMember(dest => dest.mailAddress, opt => opt.MapFrom(src => src.mailAddress));

            CreateMap<User, UserViewModel>().
               ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id)).
               ForMember(dest => dest.creationDate, opt => opt.MapFrom(src => src.creationDate)).
               ForMember(dest => dest.updatedDate, opt => opt.MapFrom(src => src.updatedDate)).
               ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username)).
               ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name)).
               ForMember(dest => dest.surname, opt => opt.MapFrom(src => src.surname)).
               ForMember(dest => dest.mailAddress, opt => opt.MapFrom(src => src.mailAddress)).
               ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive)).
               ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.isDeleted));
        }
    }
}
