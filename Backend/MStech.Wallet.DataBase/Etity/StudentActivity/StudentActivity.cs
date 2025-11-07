using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Class;
using MStech.Wallet.DataBase.Etity.StudentActivity.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStech.Wallet.DataBase.Etity.StudentActivity
{
    public class StudentActivity : BaseEntity<int>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int ClassId { get; set; }
        public string? ActivityType { get; set; } // Assignment, Quiz, Exam, Project, etc.
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxScore { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? Attachments { get; set; } // File paths or URLs for attachments
        public string? StudentId { get; set; } // UserId of the student this activity is assigned to
        
        // Navigation properties
        public InstitutionClass? Class { get; set; }
        public User? CreatedBy { get; set; }
        public User? Student { get; set; } // Navigation property for the student
        public string? CreatedById { get; set; }
        public ICollection<StudentActivitySubmission> Submissions { get; set; }
    }
}