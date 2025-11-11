using Entity.Base;
using Mstech.Entity.Etity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.ViewModel.DTO
{
    public class InstitutionDocumentViewModel : BaseEntity<int>
    {
        public bool IsActive { get; set; }
        
        // Owner information
        public string? OwnerId { get; set; }
        public string? OwnerName { get; set; } // Full name to be displayed
        public string? OwnerUserName { get; set; }
        
        // Institution Class information
        public int InstitutionClassId { get; set; }
        public string? InstitutionClassName { get; set; } // For display
        
        // Pagination parameters
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        
        // Search parameters
        public string? SearchTitle { get; set; }
        public int? InstitutionClassIdFilter { get; set; }
        public string? OwnerIdFilter { get; set; }
        public bool? IsActiveFilter { get; set; }
    }
}