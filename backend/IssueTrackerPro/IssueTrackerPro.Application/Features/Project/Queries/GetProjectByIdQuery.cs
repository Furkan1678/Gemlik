using MediatR;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Application.Features.Project.Queries
{
    public class GetProjectByIdQuery : IRequest<IssueTrackerPro.Domain.Entities.Project> // Tam nitelikli isim
    {
        public int Id { get; set; }
    }
}