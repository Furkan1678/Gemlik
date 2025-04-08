using MediatR;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Application.Features.Issue.Queries
{
    public class GetIssuesByProjectQuery : IRequest<IEnumerable<IssueTrackerPro.Domain.Entities.Issue>> // Tam nitelikli isim
    {
        public int ProjectId { get; set; }
    }
}