using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Wallet;

namespace DataBase.EntityMaps
{
    public class DiscountCodeBankMap : IEntityTypeConfiguration<DiscountCodeBank>
    {
        public void Configure(EntityTypeBuilder<DiscountCodeBank> entityBuilder)
        {
            entityBuilder.ToTable("DiscountCodeBanks");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.Owner).WithMany(m => m.DiscountCodeBanks).HasForeignKey(m => m.OwnerId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
