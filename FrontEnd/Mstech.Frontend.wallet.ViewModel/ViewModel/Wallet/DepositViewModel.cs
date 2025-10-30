using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.wallet.ViewModel.Wallet
{
    public class DepositViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "مقدار باید بیشتر از صفر باشد.")]
        [Display(Name = "مبلغ")]
        public decimal Amount { get; set; }
        public int? UserId { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "شماره فاکتور")]
        public int? InvoiceId { get; set; }
        [Display(Name = "نام کسب و کار")]
        public int? ClientId { get; set; }

        [StringLength(500, ErrorMessage = "توضیحات نمی‌تواند بیش از 500 کاراکتر باشد.")]
        [Display(Name = "توضیحات سند")]
        public string Description { get; set; }
    }
}
