using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Application.DTOs;

namespace IssueTrackerPro.Application.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}