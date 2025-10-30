using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;
public class GetAllOfMyUsersViewModel
{
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; }

    [Display(Name = "نام ")]
    public string UserFulName { get; set; }
}


