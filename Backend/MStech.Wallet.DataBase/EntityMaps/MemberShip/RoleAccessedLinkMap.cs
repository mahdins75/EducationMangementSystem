using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;

namespace DataBase.EntityMaps
{
    public class RoleAccessedLinkMap : IEntityTypeConfiguration<RoleAccessedLink>
    {
        public void Configure(EntityTypeBuilder<RoleAccessedLink> entityBuilder)
        {
            entityBuilder.ToTable("RoleAccessedLink");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.AccessedLink).WithMany(m => m.RoleAccessedLinks).HasForeignKey(m => m.AccessedLinkId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.Role).WithMany(m => m.RoleAccessedLinks).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
