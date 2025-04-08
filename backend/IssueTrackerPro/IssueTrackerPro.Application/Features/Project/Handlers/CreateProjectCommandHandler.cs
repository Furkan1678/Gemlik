using IssueTrackerPro.Application.Features.Project.Commands;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Project.Handlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new IssueTrackerPro.Domain.Entities.Project // Tam nitelikli isim
            {
                Name = request.Name,
                Description = request.Description
            };

            _projectRepository.Add(project);
            return project.Id;
        }
    }
}