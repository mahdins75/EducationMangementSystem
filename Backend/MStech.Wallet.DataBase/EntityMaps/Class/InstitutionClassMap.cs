using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MStech.Wallet.DataBase.Etity.Class;

namespace DataBase.EntityMaps
{
    public class InstitutionClassMap : IEntityTypeConfiguration<InstitutionClass>
    {
        public void Configure(EntityTypeBuilder<InstitutionClass> entityBuilder)
        {
            entityBuilder.ToTable("InstitutionClasses");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            entityBuilder.Property(x => x.Description).HasMaxLength(1000);
            entityBuilder.Property(x => x.ClassName).HasMaxLength(255);
            entityBuilder.Property(x => x.ClassCode).HasMaxLength(100);
            entityBuilder.Property(x => x.TeacherId).HasMaxLength(450);
            
            // Relationships
            entityBuilder.HasOne(m => m.Institution)
                         .WithMany(m => m.Classes)
                         .HasForeignKey(m => m.InstitutionId)
                         .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.HasOne(m => m.Teacher)
                         .WithMany()
                         .HasForeignKey(m => m.TeacherId)
                         .OnDelete(DeleteBehavior.Restrict);
                         
             entityBuilder.HasMany(m => m.InstitutionDocuments)
            .WithOne(m=>m.InstitutionClass)
            .HasForeignKey(m => m.InstitutionClassId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}