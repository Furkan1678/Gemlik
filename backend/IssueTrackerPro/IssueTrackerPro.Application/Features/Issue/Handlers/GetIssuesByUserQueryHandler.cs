using IssueTrackerPro.Application.Features.Issue.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Issue.Handlers
{
    public class GetIssuesByUserQueryHandler : IRequestHandler<GetIssuesByUserQuery, IEnumerable<IssueTrackerPro.Domain.Entities.Issue>> // Tam nitelikli isim
    {
        private readonly IIssueRepository _issueRepository;

        public GetIssuesByUserQueryHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<IEnumerable<IssueTrackerPro.Domain.Entities.Issue>> Handle(GetIssuesByUserQuery request, CancellationToken cancellationToken)
        {
            return _issueRepository.GetByUserId(request.UserId);
        }
    }
}