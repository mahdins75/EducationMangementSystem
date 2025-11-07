using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MStech.Wallet.DataBase.Etity.StudentActivity;

namespace DataBase.EntityMaps
{
    public class StudentActivityMap : IEntityTypeConfiguration<StudentActivity>
    {
        public void Configure(EntityTypeBuilder<StudentActivity> entityBuilder)
        {
            entityBuilder.ToTable("StudentActivities");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Title).IsRequired().HasMaxLength(255);
            entityBuilder.Property(x => x.Description).HasMaxLength(2000);
            entityBuilder.Property(x => x.ActivityType).HasMaxLength(100);
            entityBuilder.Property(x => x.Attachments).HasMaxLength(1000);
            entityBuilder.Property(x => x.CreatedById).HasMaxLength(450);
            
            // Relationships
            entityBuilder.HasOne(m => m.Class)
                         .WithMany(m => m.Classes)
                         .HasForeignKey(m => m.ClassId)
                         .OnDelete(DeleteBehavior.Cascade);
            
            entityBuilder.HasOne(m => m.CreatedBy)
                         .WithMany()
                         .HasForeignKey(m => m.CreatedById)
                         .OnDelete(DeleteBehavior.Restrict);
            
            entityBuilder.HasOne(m => m.Student)
                         .WithMany()
                         .HasForeignKey(m => m.StudentId)
                         .OnDelete(DeleteBehavior.Restrict);
            
            entityBuilder.HasMany(m => m.Submissions)
                         .WithOne(m => m.Activity)
                         .HasForeignKey(m => m.ActivityId)
                         .OnDelete(DeleteBehavior.Cascade);
        }
    }
}