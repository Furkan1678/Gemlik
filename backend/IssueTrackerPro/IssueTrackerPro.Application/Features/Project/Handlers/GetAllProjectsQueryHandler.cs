using IssueTrackerPro.Application.Features.Project.Queries;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using MediatR;

namespace IssueTrackerPro.Application.Features.Project.Handlers
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<IssueTrackerPro.Domain.Entities.Project>> // Tam nitelikli isim
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<IssueTrackerPro.Domain.Entities.Project>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return _projectRepository.GetAll();
        }
    }
}