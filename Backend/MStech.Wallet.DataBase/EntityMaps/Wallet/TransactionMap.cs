using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Wallet;

namespace DataBase.EntityMaps
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> entityBuilder)
        {
            entityBuilder.ToTable("Transaction");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.Parent).WithMany(m => m.Children).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.TransactionPartyWalle).WithMany(m => m.TransactionPartyWallets).HasForeignKey(m => m.TransactionPartyWalleId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Children).WithOne(m => m.Parent).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(m => m.Wallet).WithMany(m => m.Transactions).HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
