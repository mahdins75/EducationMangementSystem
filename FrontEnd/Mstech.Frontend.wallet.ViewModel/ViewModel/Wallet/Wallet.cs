using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;
public class WalletViewModel
{
    public int Id { get; set; }
    public WalletStatus ActivationStatus { get; set; }
    [Display(Name = "وضعیت")]
    public string? ActivationStatusTitle { get; set; } = string.Empty;
    public WalletType WalletType { get; set; }
    [Display(Name = "نوع کیف پول")]
    public string? WalletTypeTitle { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    [Display(Name = "")]
    public string? ParentTitle { get; set; }
    public string UserId { get; set; }
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; }
    [Display(Name = "نام")]
    public string? UserFullName { get; set; }
    public int ClientId { get; set; }
    [Display(Name = "وبسایت")]
    public string? ClientName { get; set; }
    [Display(Name = "موجودی")]
    public decimal Balance { get; set; }
    public bool Processing { get; set; }
    /// <summary>
    /// componentItems
    /// </summary>
    /// 

    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public UserViewModel User { get; set; }
    public WalletClientViewModel Client { get; set; }
    public WalletViewModel? Parent { get; set; }
    public ICollection<WalletViewModel>? Children { get; set; }
    public ICollection<TransactionViewModel>? Transactions { get; set; }

}

