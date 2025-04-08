using IssueTrackerPro.Application.Features.Comment.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Comment.Handlers
{
    public class GetCommentsByIssueQueryHandler : IRequestHandler<GetCommentsByIssueQuery, IEnumerable<IssueTrackerPro.Domain.Entities.Comment>> // Tam nitelikli isim
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByIssueQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<IssueTrackerPro.Domain.Entities.Comment>> Handle(GetCommentsByIssueQuery request, CancellationToken cancellationToken)
        {
            return _commentRepository.GetByIssueId(request.IssueId);
        }
    }
}