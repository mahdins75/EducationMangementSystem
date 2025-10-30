using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStech.Wallet.ViewModel.ViewModel.Common
{
    public class DashboarAggrigation
    {
        public DashboardWaleltsBalenceViewModel DashboardWaleltsBalence { get; set; }

        public List<ClientAccessLinkViewModel> ClientAccessLinks { get; set; }

        public List<ReferralCodeViewModel> ReferralCodes { get; set; }

        public List<WalletInfoViewModel> WalletInfos { get; set; }

    }
    public class DashboardWaleltsBalenceViewModel
    {
        public decimal Balance { get; set; }
    }

    public class ReferralCodeViewModel
    {
        public string ClientName { get; set; }
        public string WalletType { get; set; }
        public string ReferralCode { get; set; }
    }

    public class ClientAccessLinkViewModel
    {
        public string CLientName { get; set; }
        public string Link { get; set; }

    }
    public class WalletInfoViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string BalanceStatus { get; set; }
        public decimal Balence { get; set; }
        public decimal LastTransactionValue { get; set; }
    }
}
