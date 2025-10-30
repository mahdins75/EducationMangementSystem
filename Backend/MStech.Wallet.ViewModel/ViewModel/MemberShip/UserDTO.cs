using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MStech.Accounting.DataBase.Enums;
namespace Mstech.ViewModel.DTO
{
    public class UserViewModel : IdentityUser
    {
        public string? Name { get; set; } = "";
        public string? LastName { get; set; } = "";
        public string? Phone { get; set; }
        public string? RoleName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveTitle { get { return IsActive ? "فعال" : "غیر فعال"; } }
        public bool IsDeleted { get; set; }
        public int? PositionId { get; set; }
        public string? PositionName { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
        public int OrganizationId { get; set; }
        /// pagination properties
        /// 
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        /////
        ///search properties
        ///
        public int? WaleltClientId { get; set; }
        public WalletType? WalletType { get; set; }

    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور خود را وارد کنید.")]
        public string Password { get; set; }

        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? ReturnUrl { get; set; }

        public bool NeedsToChangePassowrd { get; set; }
        public bool RememberMe { get; set; }
    }


    public class TokenViewModel
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور خود را وارد کنید.")]
        public string Password { get; set; }

        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? ReturnUrl { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }

        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {

        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد کردن  کلمه عبور الزامی است")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمیباشد")]
        [Required(ErrorMessage = "وارد کردن تکرار کلمه عبور الزامی است")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "وارد کردن نام اجباری است")]
        public string? Name { get; set; } = "";

        [Required(ErrorMessage = "وارد کردن نام خانوادگی اجباری است")]
        public string? LastName { get; set; } = "";

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "فرمت ایمیل وارد شده نا معتبر است")]
        public string? Email { get; set; } = "";

        [Required(ErrorMessage = "وارد کردن شماره تلفن همراه اجباری است")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن همراه وارد شده نا معتبر است")]
        public string? PhoneNumber { get; set; } = "";

        public string? Address { get; set; } = "";

        public bool? IsActive { get; set; } = false;

        public bool IsLoginSuccess { get; set; } = false;

    }

    public class RegisterFromClientViewModel
    {

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "وارد کردن نام اجباری است")]
        public string? Name { get; set; } = "";
        public string? LastName { get; set; } = "";
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "فرمت ایمیل وارد شده نا معتبر است")]
        public string? Email { get; set; } = "";
        [Required(ErrorMessage = "وارد کردن شماره تلفن همراه اجباری است")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن همراه وارد شده نا معتبر است")]
        public string? PhoneNumber { get; set; } = "";
        public string? Address { get; set; } = "";
        public string? ClientIdForApi { get; set; }
        public bool IsLoginSuccess { get; set; } = false;
        public bool IsActive { get; set; } = false;
    }


    public class ConfirmEmailNumberViewModel
    {
        public string UserId { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Token { get; set; } = "";
    }
    public class PhoneNumberConfirmationViewModel
    {
        public string UserId { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Code { get; set; } = "";
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمیباشد")]

        public string ConfirmPassword { get; set; }
    }

    public class GeneratePasswordResetTokenViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public class GetResetPasswordCodeViewModel
    {
        public string? UserName { get; set; }

    }

    public class NeedToChangePasswordViewModel
    {
        public string? UserName { get; set; }
        public string? ConfirmationCode { get; set; }
    }
    public class WithDrawConfirmationViewModel
    {
        public string? UserName { get; set; }
        public int? WalletId { get; set; }
        public int? TransactionRequestId { get; set; }
        public string? ConfirmationCode { get; set; }

    }

}