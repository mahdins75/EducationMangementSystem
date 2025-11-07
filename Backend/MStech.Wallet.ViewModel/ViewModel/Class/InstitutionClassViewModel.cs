using Entity.Base;
using Mstech.Entity.Etity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.ViewModel.DTO
{
    public class InstitutionClassViewModel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int InstitutionId { get; set; }
        public string? ClassName { get; set; }
        public string? ClassCode { get; set; }
        public int MaxStudents { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        
        // Institution information
        public string? InstitutionName { get; set; }
        
        // Teacher information
        public string? TeacherId { get; set; }
        public string? TeacherFullName { get; set; }
        public string? TeacherUserName { get; set; }
        
        // Student information
        public int StudentsCount { get; set; }
        
        // Pagination parameters
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        
        // Navigation properties in ViewModel
        public ICollection<User> Students { get; set; }
        
        // Search parameters
        public string? SearchName { get; set; }
        public string? SearchClassCode { get; set; }
        public int? InstitutionIdFilter { get; set; }
        public bool? IsActiveFilter { get; set; }
    }
}