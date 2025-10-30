using Entity.Base;
using MStech.Accounting.DataBase.Enums;
namespace MStech.Wallet.DataBase.Etity.Wallet;

public class Transaction : BaseEntity<int>
{
    public TransactionType TransactionType { get; set; }
    public WalletTransactionCalculationType WalletTransactionCalculationType { get; set; }
    public decimal Amount { get; set; }
    public decimal LatestBalance { get; set; }
    public string JsonTransActionDataFromClient { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public int? ParentId { get; set; }
    public int? WalletId { get; set; }
    public int? TransactionPartyWalleId { get; set; }
    public bool? IsWalletValue { get; set; }
    public bool? NotMentionedInReport { get; set; }
    public int? InvioceId { get; set; }
    public DateTime? DueDateTime { get; set; }
    public string? InvioceTitle { get; set; }
    public string? TransactionSource { get; set; }
    public Wallet Wallet { get; set; }
    public Wallet TransactionPartyWalle { get; set; }
    public Transaction Parent { get; set; }
    public ICollection<Transaction> Children { get; set; }



}
