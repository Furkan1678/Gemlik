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
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (int)src.Priority));
            CreateMap<IssueDto, Issue>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (IssueStatus)src.Status))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (Priority)src.Priority));
        }
    }
}