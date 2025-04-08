using MediatR;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Application.Features.User.Queries
{
    public class GetUserByIdQuery : IRequest<IssueTrackerPro.Domain.Entities.User> // Tam nitelikli isim
    {
        public int Id { get; set; }
    }
}