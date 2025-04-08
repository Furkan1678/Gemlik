using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Domain.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        Comment GetById(int id);
        IEnumerable<Comment> GetByIssueId(int issueId);
    }
}