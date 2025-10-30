using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;
using MStech.Wallet.DataBase.Etity.Client;
namespace Mstech.ViewModel.DTO
{
    public class WalletViewModel : BaseEntity<int>
    {
        public WalletStatus ActivationStatus { get; set; }
        public string? ActivationStatusTitle { get; set; } = string.Empty;
        public WalletType WalletType { get; set; }
        public string? WalletTypeTitle { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string? ParentTitle { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? UserFullName { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public decimal Balance { get; set; }
        public UserViewModel User { get; set; }
        public WalletClientViewModel Client { get; set; }
        public WalletViewModel? Parent { get; set; }
        public ICollection<WalletViewModel>? Children { get; set; }
        public ICollection<TransactionViewModel>? Transactions { get; set; }
        //// pagination paremeters 
        ///
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        //// filtering paremeters 
        ///
        public string ClientOwnerUserName { get; set; }
    }
}
