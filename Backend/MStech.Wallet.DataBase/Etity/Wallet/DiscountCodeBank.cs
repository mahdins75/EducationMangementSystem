using Entity.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;

namespace MStech.Wallet.DataBase.Etity.Wallet
{
    public class DiscountCodeBank : BaseEntity<int>
    {
        public string DiscountCodeText { get; set; }

        public string? Title { get; set; }

        public string? ItemId { get; set; }

        public string? UnitId { get; set; }
        public DiscountCodeBankSpendType? DiscountCodeBankSpendType { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime ExpireDate { get; set; }

        public string ClientIdForApi { get; set; }

        public string? OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<DiscountCode> DiscountCodes { get; set; }



    }
}
