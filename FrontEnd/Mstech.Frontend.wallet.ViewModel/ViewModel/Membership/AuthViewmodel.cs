using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "نام")]
        public string? Name { get; set; } = "";
        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; } = "";
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است")]
        public string? UserName { get; set; } = "";
        [Display(Name = "نام کامل")]
        public string? FullName { get { return this.Name + " " + this.LastName; } }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "وارد کردن شماره تلفن الزامی است")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "نقش ")]
        public string RoleName { get; set; }
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "وارد کردن تکرار کلمه عبور الزامی است")]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمیباشد")]
        [Required(ErrorMessage = "وارد کردن تکرار کلمه عبور الزامی است")]
        public string? ConfirmPassword { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "وضعیت")]
        public string IsActiveTitle { get { return IsActive ? "فعال" : "غیر فعال"; } }
        public bool IsDeleted { get; set; }
        public int? PositionId { get; set; }
        [Display(Name = "نقش")]
        public string? PositionName { get; set; }
        public bool RememberMe { get; set; }
        public bool OnlyCurrentUser { get; set; } = false;
        public int OrganizationId { get; set; }
        /// pagination properties
        /// 
        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }

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

        public bool RememberMe { get; set; }
        public bool NeedsToChangePassowrd { get; set; }

        public string? ConfirmationCode { get; set; }

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

        [Required(ErrorMessage = "وارد کردن ایمیل اجباری است")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "فرمت ایمیل وارد شده نا معتبر است")]
        public string? Email { get; set; } = "";

        [Required(ErrorMessage = "وارد کردن شماره تلفن همراه اجباری است")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن همراه وارد شده نا معتبر است")]
        public string? PhoneNumber { get; set; } = "";
        public string? Address { get; set; } = "";

        public bool IsLoginSuccess { get; set; } = false;

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
    public class ConfirmPhoneNumberViewModel
    {
        public string UserName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Code { get; set; } = "";
        public string ResetPasswordToken { get; set; } = "";
        public bool? Status { get; set; }
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