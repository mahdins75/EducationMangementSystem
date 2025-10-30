using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.wallet.ViewModel.Wallet
{
    public class WithdrawalViewModel
    {
        public int WalletId { get; set; }
        public int TransactionRequestId { get; set; }
        public string CurrentUserName { get; set; }
        public string ConfirmationCode  { get; set; }

    }
}
