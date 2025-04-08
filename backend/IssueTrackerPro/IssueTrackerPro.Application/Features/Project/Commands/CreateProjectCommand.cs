using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IssueTrackerPro.Application.Features.Project.Commands
{
    public class CreateProjectCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}