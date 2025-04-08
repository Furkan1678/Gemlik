using IssueTrackerPro.Application.Features.Issue.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Issue.Handlers
{
    public class GetIssueByIdQueryHandler : IRequestHandler<GetIssueByIdQuery, IssueTrackerPro.Domain.Entities.Issue> // Tam nitelikli isim
    {
        private readonly IIssueRepository _issueRepository;

        public GetIssueByIdQueryHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<IssueTrackerPro.Domain.Entities.Issue> Handle(GetIssueByIdQuery request, CancellationToken cancellationToken)
        {
            return _issueRepository.GetById(request.Id);
        }
    }
}