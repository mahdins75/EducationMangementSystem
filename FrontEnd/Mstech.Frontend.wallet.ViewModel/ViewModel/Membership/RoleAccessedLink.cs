

using Mstech.Frontend.Wallet.ViewModel.DTO;

namespace Mstech.FrontEnd.Wallet.ViewModel.Membership
{
    public class RoleAccessedLinkViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AccessedLinkId { get; set; }
        public string Icon { get; set; } = "";
        public int? ParentId { get; set; }

        public bool? IsChecked { get; set; }

        public RoleViewModel? Role { get; set; }
        public AccessedLinkViewModel? AccessedLink { get; set; }
        public RoleAccessedLinkViewModel? Parent { get; set; }


    }

    public class CreateRoleAccessLinkViewmodel
    {
        public List<int> Links { get; set; }
        public int RoleId { get; set; }
    }
}