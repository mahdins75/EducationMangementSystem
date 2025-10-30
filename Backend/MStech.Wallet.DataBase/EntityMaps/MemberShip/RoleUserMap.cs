using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;

namespace DataBase.EntityMaps
{
    public class RoleUserMap : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> entityBuilder)
        {
            entityBuilder.ToTable("RoleUsers");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.Role).WithMany(m => m.RoleUsers).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
