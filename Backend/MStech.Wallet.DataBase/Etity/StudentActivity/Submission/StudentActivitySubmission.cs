using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.StudentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStech.Wallet.DataBase.Etity.StudentActivity.Submission
{
    public class StudentActivitySubmission : BaseEntity<int>
    {
        public int ActivityId { get; set; }
        public string? StudentId { get; set; }
        public string? SubmissionContent { get; set; } // Text content, file paths, etc.
        public DateTime SubmissionDate { get; set; }
        public int? Score { get; set; }
        public string? Comments { get; set; }
        public bool IsGraded { get; set; }
        public bool IsDeleted { get; set; }
        
        // Navigation properties
        public StudentActivity? Activity { get; set; }
        public User? Student { get; set; }
    }
}