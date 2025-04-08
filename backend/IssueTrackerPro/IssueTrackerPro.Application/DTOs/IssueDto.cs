using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerPro.Application.DTOs
{
    public class IssueDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int? AssignedUserId { get; set; }
        public int ProjectId { get; set; }

    }
}