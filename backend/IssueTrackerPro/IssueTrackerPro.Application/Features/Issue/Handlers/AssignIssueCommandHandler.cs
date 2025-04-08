using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Application.Features.Issue.Commands;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Issue.Handlers
{
    public class AssignIssueCommandHandler : IRequestHandler<AssignIssueCommand>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;

        public AssignIssueCommandHandler(IIssueRepository issueRepository, IUserRepository userRepository)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(AssignIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = _issueRepository.GetById(request.IssueId);
            var user = _userRepository.GetById(request.UserId);
            if (issue == null || user == null) throw new Exception("Talep veya kullanıcı bulunamadı");

            issue.AssignedUserId = request.UserId;
            _issueRepository.Update(issue);
        }
    }
}