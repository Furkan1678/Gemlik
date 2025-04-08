using IssueTrackerPro.Application.Features.Comment.Commands;
using IssueTrackerPro.Application.Features.Comment.Queries;
using IssueTrackerPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
        {
            var commentId = await _mediator.Send(command);
            return Ok(commentId);
        }

        [HttpGet("issue/{issueId}")]
        public async Task<IActionResult> GetCommentsByIssue(int issueId)
        {
            var comments = await _mediator.Send(new GetCommentsByIssueQuery { IssueId = issueId });
            return Ok(comments);
        }
    }
}