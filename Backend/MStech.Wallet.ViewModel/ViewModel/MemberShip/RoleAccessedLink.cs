using Mstech.ViewModel.DTO;

namespace Mstech.ViewModel.DTO
{
    public class RoleAccessedLinkViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AccessedLinkId { get; set; }
        public string Icon { get; set; } = "";
        public int? ParentId { get; set; }
        public RoleViewModel? Role { get; set; }
        public AccessedLinkViewModel? AccessedLink { get; set; }
        public RoleAccessedLinkViewModel? Parent { get; set; }

        /// pagination properties
        /// 
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }


    }

    public class CreateRoleAccessLinkViewmodel
    {
        public List<int> Links { get; set; }
        public int RoleId { get; set; }
    }
}