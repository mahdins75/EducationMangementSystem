using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;
using MStech.Wallet.DataBase.Etity.Client;
namespace MStech.Wallet.DataBase.Etity.Wallet
{
    public class Wallet : BaseEntity<int>
    {
        public WalletStatus ActivationStatus { get; set; }
        public WalletType WalletType { get; set; }
        public int? ParentId { get; set; }
        public string UserId { get; set; }
        public int ClientId { get; set; }
        public decimal Balance { get; set; }
        public bool Processing { get; set; }
        public User User { get; set; }
        public WalletClient Client { get; set; }
        public Wallet? Parent { get; set; }
        public ICollection<Wallet>? Children { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

        public ICollection<Transaction> TransactionPartyWallets { get; set; }
        public ICollection<ReferralCode> ReferralCodes { get; set; }


    }
}
