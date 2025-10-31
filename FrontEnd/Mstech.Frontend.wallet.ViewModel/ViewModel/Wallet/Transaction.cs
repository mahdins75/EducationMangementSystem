using System.ComponentModel.DataAnnotations;
using Mstech.Wallet.Common;
namespace Mstech.Frontend.Wallet.ViewModel.DTO;


public class TransactionViewModel
{
    public int Id { get; set; }
    [Display(Name = "شرح تراکنش")]
    public TransactionType TransactionType { get; set; }
    public WalletTransactionCalculationType WalletTransactionCalculationType { get; set; }
    [Display(Name = "شرح تراکنش")]
    public string? TransactionTypeTitle { get; set; } = string.Empty;
    [Display(Name = "شرح تراکنش")]
    public string? WalletTransactionCalculationTypeTitle { get; set; } = string.Empty;
    [Display(Name = "مبلغ")]
    public decimal Amount { get; set; }
    public string? NoDigitAmount { get { return this.Amount.ToNoDecimalString(); } }

    [Display(Name = "مبلغ")]
    public string? AmountText { get { return WalletTransactionCalculationType == WalletTransactionCalculationType.Deposit ? "+" + NoDigitAmount : "-" + NoDigitAmount; } }
    public string JsonTransActionDataFromClient { get; set; }
    public DateTime TransactionDateTime { get; set; }
    [Display(Name = "زمان تراکنش")]
    public string? TransactionDateTimeText { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public int? WalletId { get; set; }
    public int? TransactionPartyWalletId { get; set; }
    public bool? IsWalletValue { get; set; }
    public bool? NotMentionedInReport { get; set; }
    public string? WalletOwnerUserName { get; set; } = string.Empty;
    public string? WalletOwnerId { get; set; } = string.Empty;
    public int? InvoiceId { get; set; }

    [Display(Name = "منشاء تراکنش")]
    public string? TransactionSource { get; set; }
    [Display(Name = "شماره فاکتور")]
    public string? InvoiceTitle { get; set; }
    public DateTime? DueDateTime { get; set; }
    [Display(Name = "تاریخ سررسید")]
    public string? PersianTextDueDateTime { get; set; }
    public string? ClientApiId { get; set; }

    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
    [Display(Name = "طرف حساب")]
    public string? TransactionPartyUserName { get; set; } = string.Empty;

    public string? TransactionPartyOwnerId { get; set; } = string.Empty;
    public string? ExternalPaymentUrl { get; set; } = string.Empty;
    /// <summary>
    /// componentItems
    /// </summary>
    /// 
    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public WalletViewModel Wallet { get; set; }
    public TransactionViewModel Parent { get; set; }
    public ICollection<TransactionViewModel> Children { get; set; }

}
