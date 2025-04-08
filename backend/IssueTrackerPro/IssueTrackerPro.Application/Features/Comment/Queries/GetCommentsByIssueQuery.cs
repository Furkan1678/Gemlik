using MediatR;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Application.Features.Comment.Queries
{
    public class GetCommentsByIssueQuery : IRequest<IEnumerable<IssueTrackerPro.Domain.Entities.Comment>> // Tam nitelikli isim
    {
        public int IssueId { get; set; }
    }
}