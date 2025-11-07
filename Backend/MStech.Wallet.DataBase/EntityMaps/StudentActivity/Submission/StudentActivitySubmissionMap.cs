using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MStech.Wallet.DataBase.Etity.StudentActivity.Submission;

namespace DataBase.EntityMaps
{
    public class StudentActivitySubmissionMap : IEntityTypeConfiguration<StudentActivitySubmission>
    {
        public void Configure(EntityTypeBuilder<StudentActivitySubmission> entityBuilder)
        {
            entityBuilder.ToTable("StudentActivitySubmissions");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.SubmissionContent).HasMaxLength(5000);
            entityBuilder.Property(x => x.Comments).HasMaxLength(1000);
            entityBuilder.Property(x => x.StudentId).HasMaxLength(450);
            
            // Relationships
            entityBuilder.HasOne(m => m.Activity)
                         .WithMany(m => m.Submissions)
                         .HasForeignKey(m => m.ActivityId)
                         .OnDelete(DeleteBehavior.Cascade);
            
            entityBuilder.HasOne(m => m.Student)
                         .WithMany()
                         .HasForeignKey(m => m.StudentId)
                         .OnDelete(DeleteBehavior.Restrict);
        }
    }
}