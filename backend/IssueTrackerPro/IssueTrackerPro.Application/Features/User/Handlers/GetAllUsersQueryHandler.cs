using IssueTrackerPro.Application.Features.User.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.User.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<IssueTrackerPro.Domain.Entities.User>> // Tam nitelikli isim
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<IssueTrackerPro.Domain.Entities.User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll();
        }
    }
}