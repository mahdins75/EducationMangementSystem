using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;

namespace DataBase.EntityMaps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.ToTable("AspNetUsers");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.Position).WithMany(m => m.Users).HasForeignKey(m => m.PositionId).OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasMany(m => m.ReferralCodes).WithOne(m => m.User).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.DiscountCodes).WithOne(m => m.User).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Wallets).WithOne(m => m.User).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Clients).WithOne(m => m.Owner).HasForeignKey(m => m.OwnerId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.StudentActivities).WithOne(m => m.Student).HasForeignKey(m => m.StudentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.InstitutionClasses).WithOne(m => m.Teacher).HasForeignKey(m => m.TeacherId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
