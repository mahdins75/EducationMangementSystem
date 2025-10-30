using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;


namespace Mstech.Frontend.wallet.ViewModel.ViewModel.Wallet
{
    public class TransferViewModel
    {
        [Required(ErrorMessage = "مقدار مالیات اجباری است.")]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "نام کاربری اجباری است.")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }
    }
}
