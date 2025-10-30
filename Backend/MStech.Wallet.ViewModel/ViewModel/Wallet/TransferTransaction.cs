using MStech.Accounting.DataBase.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mstech.ViewModel.DTO;


public class TransferTransactionViewModel
{
    public TransactionType TransactionType { get; set; }
    public WalletTransactionCalculationType WalletTransactionCalculationType { get; set; }
    [Display(Name = "شرح تراکنش")] public string? TransactionTypeTitle { get; set; } = string.Empty;
    [Display(Name = "شرح تراکنش")] public string? WalletTransactionCalculationTypeTitle { get; set; } = string.Empty;
    [Display(Name = "مبلغ")] public decimal Amount { get; set; }

    [Display(Name = "مبلغ")]
    public string? AmountText
    {
        get
        {
            return WalletTransactionCalculationType == WalletTransactionCalculationType.Deposit
                ? "+" + Amount.ToString()
                : "-" + Amount.ToString();
        }
    }

    public int? SenderWalletId { get; set; }
    [Display(Name = "نام دریافت کننده")]
    public string? RecieverWalletUserName { get; set; }
    public bool? IsWalletValue { get; set; }
    [Display(Name = "شرح تراکنش")] public string? Description { get; set; }
}