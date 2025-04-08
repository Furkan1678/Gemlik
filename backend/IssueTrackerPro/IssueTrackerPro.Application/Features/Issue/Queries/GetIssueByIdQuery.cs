using MediatR;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Application.Features.Issue.Queries
{
    public class GetIssueByIdQuery : IRequest<IssueTrackerPro.Domain.Entities.Issue> // Tam nitelikli isim
    {
        public int Id { get; set; }
    }
}