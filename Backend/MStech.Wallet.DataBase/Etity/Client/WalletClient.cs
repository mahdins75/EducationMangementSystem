using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;
using MStech.Wallet.DataBase.Etity.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStech.Wallet.DataBase.Etity.Client
{
    public class WalletClient : BaseEntity<int>
    {
        public string Title { get; set; }
        public ClientStatus ActivationStatus { get; set; }
        public string OwnerId { get; set; }
        public string ClientIdForApi { get; set; }
        public string BaseUrl { get; set; }
        public User Owner { get; set; }
        public ICollection<Wallet.Wallet> Wallets { get; set; }
    }
}
