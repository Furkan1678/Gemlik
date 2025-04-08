using IssueTrackerPro.Application.Features.Project.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Project.Handlers
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, IssueTrackerPro.Domain.Entities.Project> // Tam nitelikli isim
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IssueTrackerPro.Domain.Entities.Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            return _projectRepository.GetById(request.Id);
        }
    }
}