using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IssueTrackerPro.Application.Features.Issue.Commands
{
    public class CreateIssueCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; }
        public int CreatedByUserId { get; set; }
    }
}