using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IssueTrackerPro.Application.Features.Project.Commands
{
    public class AssignUserToProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}