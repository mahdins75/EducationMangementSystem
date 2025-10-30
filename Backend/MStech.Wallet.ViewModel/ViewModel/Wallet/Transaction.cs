using Entity.Base;
using MStech.Accounting.DataBase.Enums;
using Mstech.Wallet.Common;
namespace Mstech.ViewModel.DTO;

public class TransactionViewModel : BaseEntity<int>
{
    public TransactionType TransactionType { get; set; }
    public WalletTransactionCalculationType WalletTransactionCalculationType { get; set; }
    public WalletType? WalletType { get; set; }
    public string? TransactionTypeTitle { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? NoDigitAmount { get { return this.Amount.ToNoDecimalString(); } }
    public string JsonTransActionDataFromClient { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public string? TransactionDateTimeText { get; set; } = string.Empty;
    public DateTime? DueDateTime { get; set; }
    public string? PersianTextDueDateTime { get; set; }
    public int? ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public int? WalletId { get; set; }
    public int? TransactionPartyWalletId { get; set; }
    public bool? IsWalletValue { get; set; }
    public bool? NotMentionedInReport { get; set; }
    public string? WalletOwnerUserName { get; set; } = string.Empty;
    public string? WalletOwnerId { get; set; } = string.Empty;
    public string? TransactionPartyUserName { get; set; } = string.Empty;

    public string? TransactionPartyOwnerId { get; set; } = string.Empty;

    public int? InvoiceId { get; set; }
    public string? InvoiceTitle{ get; set; }
    public string? TransactionSource { get; set; }
    public string? ClientApiId { get; set; }
    public string? Description { get; set; }
    public string ExternalPaymentUrl { get; set; }
    //// pagination paremeters 
    ///
    public bool IsPagination { get; set; } = false;

    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public WalletViewModel Wallet { get; set; }
    public WalletViewModel TransactionPartyWalle { get; set; }
    public TransactionViewModel Parent { get; set; }
    public ICollection<TransactionViewModel> Children { get; set; }
}