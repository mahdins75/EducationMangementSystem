using Mstech.Frontend.Wallet.ViewModel.DTO;
namespace Mstech.FrontEnd.Wallet.ViewModel.Membership
{
    public class RoleUserViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public int RoleId { get; set; }
        /// <summary>
        /// pagination parameters
        /// </summary>
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }


        public RoleViewModel? Role { get; set; }
        public UserViewModel? User { get; set; }

    }
}