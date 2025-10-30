using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Wallet;

namespace DataBase.EntityMaps
{
    public class DiscountCodeMap : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> entityBuilder)
        {
            entityBuilder.ToTable("DiscountCode");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.User).WithMany(m => m.DiscountCodes).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.DiscountCodeBank).WithMany(m => m.DiscountCodes).HasForeignKey(m => m.DiscountCodeBankId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
