using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MStech.Wallet.DataBase.Etity.Institution;

namespace DataBase.EntityMaps
{
    public class InstitutionDocumentsMap : IEntityTypeConfiguration<InstitutionDocument>
    {
        public void Configure(EntityTypeBuilder<InstitutionDocument> entityBuilder)
        {
            entityBuilder.ToTable("InstitutionDocuments");
            entityBuilder.HasKey(x => x.Id);
            
            // Relationships
            entityBuilder.HasOne(m => m.InstitutionClass)
                         .WithMany(m=>m.InstitutionDocuments)
                         .HasForeignKey(m => m.InstitutionClassId)
                         .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}