using Entity.Base;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;
namespace MStech.Wallet.DataBase.Etity.Wallet
{
    public class ReferralCodeViewModel : BaseEntity<int>
    {
        public string UserId { get; set; }
        public string? UserFullName { get; set; }

        public int WalletId { get; set; }

        public string WalletTitle { get; set; }

        public string ReferralCodeText { get; set; }

        //// pagination paremeters 
        ///
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public UserViewModel User { get; set; }
        public WalletViewModel Wallet { get; set; }
    }
}
