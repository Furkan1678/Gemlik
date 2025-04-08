using IssueTrackerPro.Application.Features.User.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.User.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IssueTrackerPro.Domain.Entities.User> // Tam nitelikli isim
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IssueTrackerPro.Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.GetById(request.Id);
        }
    }
}