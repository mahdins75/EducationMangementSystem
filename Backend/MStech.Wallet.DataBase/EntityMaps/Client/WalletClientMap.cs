using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MStech.Wallet.DataBase.Etity.Client;

namespace DataBase.EntityMaps
{
    public class WalletClientMap : IEntityTypeConfiguration<WalletClient>
    {
        public void Configure(EntityTypeBuilder<WalletClient> entityBuilder)
        {
            entityBuilder.ToTable("WalletClient");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasOne(m => m.Owner).WithMany(m => m.Clients).HasForeignKey(m => m.OwnerId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.Wallets).WithOne(m => m.Client).HasForeignKey(m => m.ClientId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
