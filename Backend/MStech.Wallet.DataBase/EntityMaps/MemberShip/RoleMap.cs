using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;

namespace DataBase.EntityMaps
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entityBuilder)
        {
            entityBuilder.ToTable("Role");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasMany(m => m.RoleUsers).WithOne(m => m.Role).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.RoleAccessedLinks).WithOne(m => m.Role).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Children).WithOne(m => m.Parent).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.Parent).WithMany(m => m.Children).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
