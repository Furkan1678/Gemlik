using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Application.Features.User.Commands;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using IssueTrackerPro.Infrastructure.Services.Security;
using MediatR;

namespace IssueTrackerPro.Application.Features.User.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(request.Id);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            user.Username = request.Username;
            user.PasswordHash = _passwordHasher.HashPassword(request.Password);
            _userRepository.Update(user);
        }
    }
}