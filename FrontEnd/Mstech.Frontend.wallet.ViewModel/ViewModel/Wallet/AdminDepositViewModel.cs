using System.ComponentModel.DataAnnotations;
using Mstech.Frontend.Wallet.ViewModel.DTO;

namespace Mstech.Frontend.wallet.ViewModel.Wallet
{
    public class AdminDepositViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "مقدار باید بیشتر از صفر باشد.")]
        public decimal Amount { get; set; }

        [StringLength(500, ErrorMessage = "توضیحات نمی‌تواند بیش از 500 کاراکتر باشد.")]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required] public WalletType WalletType { get; set; } = WalletType.Purchase;
    }
}
