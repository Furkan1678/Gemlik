using IssueTrackerPro.Application.Features.Issue.Queries;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerPro.Application.Features.Issue.Handlers
{
    public class GetAllIssuesQueryHandler : IRequestHandler<GetAllIssuesQuery, IEnumerable<IssueTrackerPro.Domain.Entities.Issue>>
    {
        private readonly IIssueRepository _issueRepository;

        public GetAllIssuesQueryHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<IEnumerable<IssueTrackerPro.Domain.Entities.Issue>> Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
        {
            return _issueRepository.GetAll(); // Tüm talepleri getir
        }
    }
}
