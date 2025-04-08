using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerPro.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; } // Yorumu yapan kullanıcı
        public int IssueId { get; set; } // Hangi talebe ait
    }
}
