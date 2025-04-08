using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IssueTrackerPro.Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}