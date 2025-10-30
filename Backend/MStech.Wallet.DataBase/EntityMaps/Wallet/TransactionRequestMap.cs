using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Etity.FileManager;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Wallet;

namespace DataBase.EntityMaps
{
    public class TransactionRequestMap : IEntityTypeConfiguration<TransactionRequest>
    {
        public void Configure(EntityTypeBuilder<TransactionRequest> entityBuilder)
        {
            entityBuilder.ToTable("TransactionRequest");
            entityBuilder.HasKey(x => x.Id);

        }
    }
}
