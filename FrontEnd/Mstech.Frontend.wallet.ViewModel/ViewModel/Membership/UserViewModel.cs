using System.ComponentModel.DataAnnotations;

namespace Mstech.FrontEnd.Wallet.ViewModel.Membership
{

    public class UserDataViewModel
    {
        public string Id { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public string? PositionName { get; set; }
        public int? RoleId { get; set; }
        public bool IsActive { get; set; }
    }

}