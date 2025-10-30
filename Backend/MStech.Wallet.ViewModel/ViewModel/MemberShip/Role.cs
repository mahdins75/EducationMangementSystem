using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mstech.ViewModel.DTO
{

    public class RoleDataTableViewModel : JqueryDataTable
    {
        public RoleFilterViewModel Filter { get; set; }
        public List<RoleViewModel> Data { get; set; }

        public RoleDataTableViewModel()
        {
            Filter = new RoleFilterViewModel();
            Data = new List<RoleViewModel>();
        }
    }
    public class RoleFilterViewModel
    {
        [Display(Name = "عنوان")]
        public string? Name { get; set; }

        [Display(Name = "نام فارسی")]
        public string? PersianName { get; set; }

        [Display(Name = "وضعیت")]
        public bool? Status { get; set; }

        public string? UserId { get; set; }

        // public IEnumerable<SelectListItem> PositionList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }

    public class RoleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "نام فارسی")]
        public string PersianName { get; set; }

        public int? ParentId { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        public bool IsSelected { get; set; }
        public List<int>? Links { get; set; }
        public RoleViewModel? Parent { get; set; }
        public ICollection<RoleUserViewModel>? RoleUsers { get; set; }
        public ICollection<RoleAccessedLinkViewModel>? RoleAccessedLinks { get; set; }
        public ICollection<RoleViewModel>? Children { get; set; }
        #region selectLists
        public IEnumerable<SelectListItem>? StatusList { get; set; }
        #endregion

        /// pagination properties
        /// 
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
    }

    public class CreateRoleIndexViewModel
    {
        public CreateRoleIndexViewModel()
        {
            ParentItemList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
            AccessedLinks = new List<LinksViewModel>();
            Links = new List<int>();
        }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است")]
        public string Name { get; set; }

        [Display(Name = "نام فارسی")]
        [Required(ErrorMessage = "وارد کردن نام فارسی اجباری است")]
        public string PersianName { get; set; }

        [Display(Name = "نقش والد")]
        public int? ParentId { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "وارد کردن وضعیت اجباری است")]
        public bool Status { get; set; } = true;

        public List<int> Links { get; set; }


        public IEnumerable<SelectListItem> ParentItemList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<LinksViewModel> AccessedLinks { get; set; }

    }

    public class EditRoleIndexViewModel : CreateRoleIndexViewModel
    {
        public int Id { get; set; }
    }

    public class LinksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public bool Selected { get; set; }
        public int Order { get; set; }

    }

}