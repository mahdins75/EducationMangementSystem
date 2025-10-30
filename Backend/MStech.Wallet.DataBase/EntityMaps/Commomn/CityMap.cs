using Mstech.Entity.Etity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.EntityMaps
{
    public class CityMap: IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> entityBuilder)
        {
            entityBuilder.ToTable("City");
            entityBuilder.HasKey(x => x.Id);
        }
    }
}