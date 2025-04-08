using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Application.Features.Issue.Commands;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Enums;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Issue.Handlers
{
    public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand>
    {
        private readonly IIssueRepository _issueRepository;

        public UpdateIssueCommandHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = _issueRepository.GetById(request.Id);
            if (issue == null) throw new Exception("Talep bulunamadı");

            issue.Title = request.Title;
            issue.Description = request.Description;
            issue.Status = (IssueStatus)request.Status;
            issue.Priority = (Priority)request.Priority;
            issue.AssignedUserId = request.AssignedUserId; // Kullanıcı atama
            _issueRepository.Update(issue);
        }
    }
}