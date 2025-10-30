using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mstech.ViewModel.DTO
{
    public class UserDataTableViewModel : JqueryDataTable
    {
        public UserFilterViewModel Filter { get; set; }
        public List<UserDataViewModel> Data { get; set; }

        public UserDataTableViewModel()
        {
            Filter = new UserFilterViewModel();
            Data = new List<UserDataViewModel>();
        }
    }

    public class UserFilterViewModel
    {
        public UserFilterViewModel()
        {
            Links = new List<string>();
        }

        [Display(Name = "نام و نام خانوادگی ")]
        public string? FullName { get; set; }

        [Display(Name = "وضعیت")]
        public bool? IsActive { get; set; }

        public List<string> Links { get; set; }

        public IEnumerable<SelectListItem> PositionList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }

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

    public class CreateUserViewModel
    {
        public CreateUserViewModel()
        {
            PositionList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
        }
        public string? Id { get; set; }

        [Display(Name = "شماره همراه")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن همراه وارد شده نا معتبر است")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "ایمیل")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "فرمت ایمیل وارد شده نا معتبر است")]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن نام اجباری است")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن نام خانوادگی اجباری است")]
        public string LastName { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "سمت")]
        public int? PositionId { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی است")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمیباشد")]
        [Required(ErrorMessage = "وارد کردن تکرار کلمه عبور الزامی است")]
        public string ConfirmPassword { get; set; }
        public string? PositionName { get; set; } = string.Empty;

        public bool IsHRMember { get; set; }


        public IEnumerable<SelectListItem> PositionList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }

    }

    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            PositionList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
        }
        public string? Id { get; set; }

        [Display(Name = "شماره همراه")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن همراه وارد شده نا معتبر است")]
        public string? Phone { get; set; }
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "وارد کردن شماره همراه اجباری است")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن همراه وارد شده نا معتبر است")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "فرمت ایمیل وارد شده نا معتبر است")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "وارد کردن ایمیل اجباری است")]
        public string UserName { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن نام اجباری است")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن نام خانوادگی اجباری است")]
        public string LastName { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        public bool EditIsHRMember { get; set; }

        [Display(Name = "سمت")]
        public int? PositionId { get; set; }
        public string? PositionName { get; set; } = string.Empty;
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی است")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمیباشد")]
        [Required(ErrorMessage = "وارد کردن تکرار کلمه عبور الزامی است")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; } = true;
        public bool OnlyCurrentUser { get; set; } = false;

        public IEnumerable<SelectListItem> PositionList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }

    }

    public class ChangePasswordUserViewModel
    {
        public ChangePasswordUserViewModel()
        {

        }

        [Required(ErrorMessage = "*")]
        public string Id { get; set; }

        [Display(Name = "کلمه عبور قبلی")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد کردن کلمه عبور قبلی الزامی است")]
        public string OldPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد کردن کلمه عبور جدید الزامی است")]
        public string Password { get; set; }
    }

    public class AjaxModalValidation
    {
        public string Property { get; set; }
        public string? Errors { get; set; }
    }

    public class ManageRoleViewModel
    {
        public ManageRoleViewModel()
        {
            Roles = new List<RoleItemViewModel>();
        }
        [HiddenInput]
        public int UserId { set; get; }

        [HiddenInput]
        public int RoleId { set; get; }

        public IEnumerable<RoleItemViewModel> Roles { set; get; }
    }

    public class RoleItemViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool UserHasThis { get; set; }
    }

    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public int[] RoleIds { get; set; }

    }

    public class EditProfileViewModel
    {
        public string Id { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "وارد کردن شماره همراه اجباری است")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن همراه وارد شده نا معتبر است")]
        public string Phone { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن ایمیل اجباری است")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "فرمت ایمیل وارد شده نا معتبر است")]
        public string Email { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن نام اجباری است")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن نام خانوادگی اجباری است")]
        public string LastName { get; set; }

        public bool IsRemovePicture { get; set; }

        public IFormFile? AvatarFile { get; set; }

        public string? AvatarSrc { get; set; }
    }
    public class ConfirmPhoneNumberViewModel
    {
        public string UserName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Code { get; set; } = "";
        public string ResetPasswordToken { get; set; } = "";
        public bool? Status { get; set; }
    }
    public class ChangePasswordProfileViewModel
    {
        public ChangePasswordProfileViewModel()
        {

        }

        [Required(ErrorMessage = "*")]
        public string Id { get; set; }

        [Display(Name = "کلمه عبور قبلی")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد کردن کلمه عبور قبلی الزامی است")]
        public string OldPassword { get; set; }

        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی است")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمیباشد")]
        [Required(ErrorMessage = "وارد کردن تکرار کلمه عبور الزامی است")]
        public string ConfirmPassword { get; set; }

    }
}