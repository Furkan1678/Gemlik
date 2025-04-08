using IssueTrackerPro.Domain.Enums;
using System;

namespace IssueTrackerPro.Domain.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }
        public Priority Priority { get; set; }
        public int? AssignedUserId { get; set; } // Atanmış kullanıcı (opsiyonel)
        public int ProjectId { get; set; } // Hangi projeye ait
        public int CreatedByUserId { get; set; } // Talebi oluşturan kullanıcı (zorunlu)
    }
}