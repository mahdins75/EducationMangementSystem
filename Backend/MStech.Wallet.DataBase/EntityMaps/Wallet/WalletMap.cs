using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Wallet;

namespace DataBase.EntityMaps
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> entityBuilder)
        {
            entityBuilder.ToTable("Wallet");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.User).WithMany(m => m.Wallets).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.Client).WithMany(m => m.Wallets).HasForeignKey(m => m.ClientId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.Parent).WithMany(m => m.Children).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Children).WithOne(m => m.Parent).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.ReferralCodes).WithOne(m => m.Wallet).HasForeignKey(m => m.WalletId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Transactions).WithOne(m => m.Wallet).HasForeignKey(m => m.WalletId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
