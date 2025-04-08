using IssueTrackerPro.Application.Features.Issue.Commands;
using IssueTrackerPro.Application.Features.Issue.Queries;
using IssueTrackerPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet] // GET metodunu açıkça belirt
        public async Task<IActionResult> GetIssues()
        {
            var issues = await _mediator.Send(new GetAllIssuesQuery());
            return Ok(issues);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueCommand command)
        {
            var issueId = await _mediator.Send(command);
            return Ok(issueId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, [FromBody] UpdateIssueCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignIssue([FromBody] AssignIssueCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var issue = await _mediator.Send(new GetIssueByIdQuery { Id = id });
            return Ok(issue);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetIssuesByProject(int projectId)
        {
            var issues = await _mediator.Send(new GetIssuesByProjectQuery { ProjectId = projectId });
            return Ok(issues);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetIssuesByUser(int userId)
        {
            var issues = await _mediator.Send(new GetIssuesByUserQuery { UserId = userId });
            return Ok(issues);
        }
    }
}