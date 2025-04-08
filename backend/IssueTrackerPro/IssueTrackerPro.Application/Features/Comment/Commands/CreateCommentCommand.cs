using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IssueTrackerPro.Application.Features.Comment.Commands
{
    public class CreateCommentCommand : IRequest<int>
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int IssueId { get; set; }
    }
}