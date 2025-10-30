using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;
using MStech.Wallet.DataBase.Etity.Client;
namespace MStech.Wallet.DataBase.Etity.Wallet
{
    public class ReferralCode : BaseEntity<int>
    {
        public string UserId { get; set; }

        public int WalletId { get; set; }

        public string ReferralCodeText { get; set; }

        public User User { get; set; }
        public Wallet Wallet { get; set; }
    }
}
