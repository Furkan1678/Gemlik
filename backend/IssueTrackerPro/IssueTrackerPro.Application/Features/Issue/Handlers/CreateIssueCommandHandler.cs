using IssueTrackerPro.Application.Features.Issue.Commands;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Enums;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTrackerPro.Application.Features.Issue.Handlers
{
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, int>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateIssueCommandHandler(IIssueRepository issueRepository, IHttpContextAccessor httpContextAccessor)
        {
            _issueRepository = issueRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Kullanıcı kimliği bulunamadı!");
            }

            var issue = new IssueTrackerPro.Domain.Entities.Issue
            {
                Title = request.Title,
                Description = request.Description,
                Status = IssueStatus.Acik,
                Priority = (Priority)request.Priority,
                ProjectId = request.ProjectId,
                CreatedByUserId = int.Parse(userId) // Token’dan gelen userId
            };

            return _issueRepository.Add(issue);
        }
    }
}