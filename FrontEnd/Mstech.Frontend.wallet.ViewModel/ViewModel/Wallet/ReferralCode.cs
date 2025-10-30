using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;
public class ReferralCode 
{
    public int Id { get; set; }

    [Display(Name = "کاربر")]
    public string UserId { get; set; }

    [Display(Name = "نام کامل کاربر")]
    public string? UserFullName { get; set; }

    [Display(Name = "کیف پول")]
    public int WalletId { get; set; }
        
    [Display(Name = "عنوان کیف پول")]
    public string WalletTitle { get; set; }

    public string ReferralCodeText { get; set; }

    public UserViewModel User { get; set; }
    public WalletViewModel Wallet { get; set; }
}
