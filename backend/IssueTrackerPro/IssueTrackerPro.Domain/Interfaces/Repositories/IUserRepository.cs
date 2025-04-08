using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTrackerPro.Domain.Entities;

namespace IssueTrackerPro.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetById(int id);
        User GetByUsername(string username);
        IEnumerable<User> GetAll();
        void Update(User user);
    }
}