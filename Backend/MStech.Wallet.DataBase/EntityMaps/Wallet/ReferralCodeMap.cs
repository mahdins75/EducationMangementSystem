using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Wallet;

namespace DataBase.EntityMaps
{
    public class ReferralCodeMap : IEntityTypeConfiguration<ReferralCode>
    {
        public void Configure(EntityTypeBuilder<ReferralCode> entityBuilder)
        {
            entityBuilder.ToTable("ReferralCode");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.User).WithMany(m => m.ReferralCodes).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.Wallet).WithMany(m => m.ReferralCodes).HasForeignKey(m => m.WalletId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
