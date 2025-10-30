using Entity.Base;
using Mstech.Entity.Etity;
namespace MStech.Wallet.DataBase.Etity.Wallet
{
    public class DiscountCode : BaseEntity<int>
    {
        public string UserId { get; set; }

        public int WalletId { get; set; }

        public int DiscountCodeBankId { get; set; }

        public DiscountCodeBank DiscountCodeBank { get; set; }
        public User User { get; set; }
    }
}
