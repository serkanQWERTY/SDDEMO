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
               ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username));
        }
    }
}
