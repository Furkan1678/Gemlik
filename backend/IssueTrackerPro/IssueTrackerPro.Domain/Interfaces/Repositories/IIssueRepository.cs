using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Domain.Interfaces.Repositories
{
    public interface IIssueRepository
    {
        IEnumerable<Issue> GetAll();
        int Add(Issue issue);
        Issue GetById(int id);
        IEnumerable<Issue> GetByProjectId(int projectId);
        IEnumerable<Issue> GetByUserId(int userId);
        void Update(Issue issue);
    }
}