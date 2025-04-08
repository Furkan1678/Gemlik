using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IssueTrackerPro.Application.Features.Issue.Commands
{
    public class AssignIssueCommand : IRequest
    {
        public int IssueId { get; set; }
        public int UserId { get; set; }
    }
}