using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Institution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStech.Wallet.DataBase.Etity.Class
{
    public class InstitutionClass : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int InstitutionId { get; set; }
        public string? ClassName { get; set; } // Specific class name like "Math 101"
        public string? ClassCode { get; set; } // Unique identifier like "MATH-101"
        public int MaxStudents { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
        // Navigation properties
        public Institution.Institution? Institution { get; set; }
        public User? Teacher { get; set; }
        public string? TeacherId { get; set; }
        public ICollection<StudentActivity.StudentActivity> Classes { get; set; }
        public ICollection<InstitutionDocument> InstitutionDocuments { get; set; }

    }
}