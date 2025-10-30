using Mstech.Entity.Etity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.EntityMaps
{
    public class ProvinceMap: IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> entityBuilder)
        {
            entityBuilder.ToTable("Province");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasMany(x => x.Cities).WithOne(x => x.Province).HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}