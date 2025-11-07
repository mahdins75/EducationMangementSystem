using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MStech.Wallet.DataBase.Etity.Institution;

namespace DataBase.EntityMaps
{
    public class InstitutionMap : IEntityTypeConfiguration<Institution>
    {
        public void Configure(EntityTypeBuilder<Institution> entityBuilder)
        {
            entityBuilder.ToTable("Institutions");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            entityBuilder.Property(x => x.Description).HasMaxLength(1000);
            entityBuilder.Property(x => x.Address).HasMaxLength(500);
            entityBuilder.Property(x => x.Phone).HasMaxLength(20);
            entityBuilder.Property(x => x.Email).HasMaxLength(100);
            entityBuilder.Property(x => x.Website).HasMaxLength(255);
            entityBuilder.Property(x => x.OwnerId).HasMaxLength(450);
            
            // Relationships
            entityBuilder.HasOne(m => m.Owner)
                         .WithMany()
                         .HasForeignKey(m => m.OwnerId)
                         .OnDelete(DeleteBehavior.Restrict);
            
            entityBuilder.HasMany(m => m.Classes)
                         .WithOne(m => m.Institution)
                         .HasForeignKey(m => m.InstitutionId)
                         .OnDelete(DeleteBehavior.Cascade);
        }
    }
}