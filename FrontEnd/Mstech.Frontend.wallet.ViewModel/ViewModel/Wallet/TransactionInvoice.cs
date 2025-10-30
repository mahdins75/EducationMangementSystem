using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class TransactionInvoiceViewModel
{
    [Display(Name = "عنوان خرید")]
    public string InvoiceTitle { get; set; }

    [Display(Name = "شناسه تراکنش")]
    public int? InvoiceId { get; set; }

    public string? JsonInvoiceData { get; set; }
}

