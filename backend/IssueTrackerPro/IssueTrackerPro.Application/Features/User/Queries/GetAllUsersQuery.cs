using MediatR;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Application.Features.User.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<IssueTrackerPro.Domain.Entities.User>> // Tam nitelikli isim
    {
    }
}