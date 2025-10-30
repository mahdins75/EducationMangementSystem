using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.wallet.ViewModel.Wallet
{
    public class WithdrawViewModel
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
