using Mstech.Entity.Etity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.EntityMaps
{
    public class ConstantMap : IEntityTypeConfiguration<Constant>
    {
        public void Configure(EntityTypeBuilder<Constant> entityBuilder)
        {
            entityBuilder.ToTable("Constants");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasMany(x => x.Users).WithOne(x => x.Position).HasForeignKey(x => x.PositionId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}