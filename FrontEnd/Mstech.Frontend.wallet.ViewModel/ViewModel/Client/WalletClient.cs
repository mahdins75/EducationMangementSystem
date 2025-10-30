using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;
public class WalletClientViewModel
{
    public int Id { get; set; }
    [Display(Name = "عنوان مشترک")]
    public string Title { get; set; }
    [Display(Name = "وضعیت فعالیت مشتری")]
    public int ActivationStatus { get; set; }
    [Display(Name = "وضعیت فعالیت مشتری")]
    public string? ActivationStatusTitle { get; set; }
    [Display(Name = "مالک پروژه")]
    public string OwnerId { get; set; }
    [Display(Name = "مالک پروژه")]
    public string OwnerFullName { get; set; }
    [Display(Name = "تعداد کیف پول های مرتبط")]
    public int WalletsCount { get; set; }
    public string ClientIdForApi { get; set; }
    public string? UserName { get; set; }
    public string BaseUrl { get; set; }
    /// <summary>
    /// pagination parameters
    /// </summary>
    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public ICollection<WalletViewModel> Wallets { get; set; }
    public UserViewModel Owner { get; set; }


    ////search parameters
    ///
  
    /// <summary>
    /// componentItems
    /// </summary>
    /// 

}

