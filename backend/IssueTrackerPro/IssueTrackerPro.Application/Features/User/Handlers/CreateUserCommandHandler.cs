using IssueTrackerPro.Application.Features.User.Commands;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Enums;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using IssueTrackerPro.Infrastructure.Services.Security;
using MediatR;

namespace IssueTrackerPro.Application.Features.User.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new IssueTrackerPro.Domain.Entities.User // Tam nitelikli isim
            {
                Username = request.Username,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Role = (UserRole)request.Role
            };

            _userRepository.Add(user);
            return user.Id;
        }
    }
}