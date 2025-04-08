using IssueTrackerPro.Application.Features.Issue.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Issue.Handlers
{
    public class GetIssuesByProjectQueryHandler : IRequestHandler<GetIssuesByProjectQuery, IEnumerable<IssueTrackerPro.Domain.Entities.Issue>> // Tam nitelikli isim
    {
        private readonly IIssueRepository _issueRepository;

        public GetIssuesByProjectQueryHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<IEnumerable<IssueTrackerPro.Domain.Entities.Issue>> Handle(GetIssuesByProjectQuery request, CancellationToken cancellationToken)
        {
            return _issueRepository.GetByProjectId(request.ProjectId);
        }
    }
}