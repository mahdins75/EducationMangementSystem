using Entity.Base;
using Mstech.Entity.Etity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.ViewModel.DTO
{
    public class InstitutionViewModel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public bool IsActive { get; set; }
        
        // Owner information
        public string? OwnerId { get; set; }
        public string? OwnerFullName { get; set; }
        public string? OwnerUserName { get; set; }
        
        // Class information
        public int ClassesCount { get; set; }
        
        // Pagination parameters
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        
        // Navigation properties in ViewModel
        public ICollection<InstitutionClassViewModel> Classes { get; set; }
        
        // Search parameters
        public string? SearchName { get; set; }
        public string? SearchEmail { get; set; }
        public bool? IsActiveFilter { get; set; }
    }
}