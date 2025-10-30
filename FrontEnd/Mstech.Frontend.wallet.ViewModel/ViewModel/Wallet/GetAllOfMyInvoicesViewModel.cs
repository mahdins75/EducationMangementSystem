using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class GetAllOfMyInvoicesViewModel
{
    [Display(Name = "شناسه سایت")]
    public string ClientId { get; set; }

    [Display(Name = "عنوان سایت")]
    public string Title { get; set; }

    [Display(Name = "کیف پول ")]
    public string WalletId { get; set; }
    [Display(Name = "شناسه کاربر")]
    public string WalletOwnerName{ get; set; }

    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
}

