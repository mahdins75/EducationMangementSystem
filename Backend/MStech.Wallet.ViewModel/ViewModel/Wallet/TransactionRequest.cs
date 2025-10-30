using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;
using MStech.Wallet.DataBase.Etity.Client;
namespace Mstech.ViewModel.DTO;

public class TransactionRequestViewModel : BaseEntity<int>
{
    public TransactionType TransactionType { get; set; }
    public WalletTransactionCalculationType WalletTransactionCalculationType { get; set; }
    public WalletType? WalletType { get; set; }
    public string? TransactionTypeTitle { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string JsonTransActionDataFromClient { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public string? TransactionDateTimeText { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public int? WalletId { get; set; }
    public bool? IsWalletValue { get; set; }
    public bool? NotMentionedInReport { get; set; }
    public string? WalletOwnerUserName { get; set; } = string.Empty;
    public string? WalletOwnerId { get; set; } = string.Empty;
    public int? InvoiceId { get; set; }
    public string? TransactionSource { get; set; }
    public string? ClientApiId { get; set; }
    public string? Description { get; set; }

    //// pagination paremeters 
    ///
    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    ////
    /// extra

    public string? ConfirmationCode { get; set; }
    public bool WaitForConfirmation { get; set; }

}
