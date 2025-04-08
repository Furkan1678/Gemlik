using IssueTrackerPro.Application.Features.Comment.Commands;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Comment.Handlers
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new IssueTrackerPro.Domain.Entities.Comment // Tam nitelikli isim
            {
                Content = request.Content,
                UserId = request.UserId,
                IssueId = request.IssueId
            };

            _commentRepository.Add(comment);
            return comment.Id;
        }
    }
}