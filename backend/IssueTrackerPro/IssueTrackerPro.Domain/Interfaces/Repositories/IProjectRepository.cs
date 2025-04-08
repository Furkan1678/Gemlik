using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Domain.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        void Add(Project project);
        Project GetById(int id);
        IEnumerable<Project> GetAll();
        void Update(Project project);
    }
}