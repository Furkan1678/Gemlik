using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Application.Features.Project.Commands;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Project.Handlers
{
    public class AssignUserToProjectCommandHandler : IRequestHandler<AssignUserToProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public AssignUserToProjectCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(AssignUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _projectRepository.GetById(request.ProjectId);
            var user = _userRepository.GetById(request.UserId);
            if (project == null || user == null) throw new Exception("Proje veya kullanıcı bulunamadı");

            // Burada bir ilişki tablosu kullanılabilir, şimdilik basit bir kontrol
        }
    }
}