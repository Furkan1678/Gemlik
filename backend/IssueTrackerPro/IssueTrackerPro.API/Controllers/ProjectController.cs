using IssueTrackerPro.Application.Features.Project.Commands;
using IssueTrackerPro.Application.Features.Project.Queries;
using IssueTrackerPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
        {
            var projectId = await _mediator.Send(command);
            return Ok(projectId);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignUserToProject([FromBody] AssignUserToProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery { Id = id });
            return Ok(project);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery());
            return Ok(projects);
        }
    }
}