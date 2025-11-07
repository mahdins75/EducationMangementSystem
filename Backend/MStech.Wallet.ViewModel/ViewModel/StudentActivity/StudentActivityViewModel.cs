using Entity.Base;
using Mstech.Entity.Etity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.ViewModel.DTO
{
    public class StudentActivityViewModel : BaseEntity<int>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int ClassId { get; set; }
        public string? ClassName { get; set; } // Class name for display
        public string? ActivityType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxScore { get; set; }
        public bool IsActive { get; set; }
        public string? Attachments { get; set; }

        // Creator information
        public string? CreatedById { get; set; }
        public string? CreatedByFullName { get; set; }
        public string? CreatedByUserName { get; set; }

        // Student information
        public string? StudentId { get; set; }
        public string? StudentFullName { get; set; } // Full name to be displayed
        public string? StudentUserName { get; set; }

        // Submission information
        public int SubmissionsCount { get; set; }
        public int SubmittedCount { get; set; }
        public int GradedCount { get; set; }

        // Pagination parameters
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }

        // Navigation properties in ViewModel
        public ICollection<StudentActivitySubmissionViewModel> Submissions { get; set; }

        // Search parameters
        public string? SearchTitle { get; set; }
        public string? ActivityTypeFilter { get; set; }
        public int? ClassIdFilter { get; set; }
        public bool? IsActiveFilter { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
    }
    


    public class CreateStudentActivityViewModel
    {
        public string? StudentId { get; set; }
        public string? StudentFullName { get; set; }
        public string? ActivityType { get; set; } = string.Empty;
        public string? ActivityTitle { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime ActivityDateTime { get; set; }
        public decimal Score { get; set; }
        public string? Status { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public string? AdditionalNotes { get; set; } = string.Empty;
    }

    public class UpdateStudentActivityViewModel
    {
        public int Id { get; set; }
        public string? StudentId { get; set; }
        public string? StudentFullName { get; set; }
        public string? ActivityType { get; set; } = string.Empty;
        public string? ActivityTitle { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime ActivityDateTime { get; set; }
        public decimal Score { get; set; }
        public string? Status { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public string? AdditionalNotes { get; set; } = string.Empty;
    }
}