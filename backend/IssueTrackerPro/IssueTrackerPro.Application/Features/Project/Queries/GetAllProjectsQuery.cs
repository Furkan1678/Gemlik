using MediatR;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Application.Features.Project.Queries
{
    public class GetAllProjectsQuery : IRequest<IEnumerable<IssueTrackerPro.Domain.Entities.Project>> // Tam nitelikli isim
    {
    }
}