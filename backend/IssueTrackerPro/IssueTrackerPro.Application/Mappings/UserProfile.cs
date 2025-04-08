using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Application.DTOs;
using IssueTrackerPro.Domain.Enums;

namespace IssueTrackerPro.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (int)src.Role));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (UserRole)src.Role));
        }
    }
}