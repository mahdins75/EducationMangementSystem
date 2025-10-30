using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;

namespace DataBase.EntityMaps
{
    public class FileStorageMap: IEntityTypeConfiguration<FileStorage>
    {
        public void Configure(EntityTypeBuilder<FileStorage> entityBuilder)
        {
            entityBuilder.ToTable("FileStorages");
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
