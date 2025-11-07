using Entity.Base;
using Mstech.Entity.Etity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.ViewModel.DTO
{
    public class StudentActivitySubmissionViewModel : BaseEntity<int>
    {
        public int ActivityId { get; set; }
        public string? StudentId { get; set; }
        public string? StudentFullName { get; set; }
        public string? StudentUserName { get; set; }
        public string? SubmissionContent { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int? Score { get; set; }
        public string? Comments { get; set; }
        public bool IsGraded { get; set; }
        
        // Pagination parameters
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        
        // Search parameters
        public string? SearchStudentName { get; set; }
        public int? ActivityIdFilter { get; set; }
        public bool? IsGradedFilter { get; set; }
    }
}