using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;

namespace DataBase.EntityMaps
{
    public class AccessedLinkMap : IEntityTypeConfiguration<AccessedLink>
    {
        public void Configure(EntityTypeBuilder<AccessedLink> entityBuilder)
        {
            entityBuilder.ToTable("AccessedLinks");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.Parent).WithMany(m => m.Children).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Children).WithOne(m => m.Parent).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.RoleAccessedLinks).WithOne(m => m.AccessedLink).HasForeignKey(m => m.AccessedLinkId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
