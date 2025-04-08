using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerPro.Application.Features.Issue.Queries
{
    public class GetAllIssuesQuery : IRequest<IEnumerable<IssueTrackerPro.Domain.Entities.Issue>>
    {
    }
}
