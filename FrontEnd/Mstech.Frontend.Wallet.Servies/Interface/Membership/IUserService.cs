using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.Membership
{
    public interface IUserService
    {
        Task<ResponseViewModel<LoginViewModel>> Login(LoginViewModel model);
        Task<ResponseViewModel<ConfirmEmailNumberViewModel>> ConfirmRegistration(ConfirmEmailNumberViewModel model);
        Task<ResponseViewModel<ConfirmPhoneNumberViewModel>> ConfirmPhoneNumber(ConfirmPhoneNumberViewModel model);
        Task<ResponseViewModel<PhoneNumberConfirmationViewModel>> CreateConfirmationCodeForPhoneNumber(PhoneNumberConfirmationViewModel model);
        Task<ResponseViewModel<ResetPasswordViewModel>> ResetPassword(ResetPasswordViewModel model);
        Task<ResponseViewModel<bool>> Logout();
        Task<ResponseViewModel<LoginViewModel>> RefreshToken();
        Task<ResponseViewModel<RegisterViewModel>> Register(RegisterViewModel model);
        Task<ResponseViewModel<LoginViewModel>> GeneratePasswordResetToken(GeneratePasswordResetTokenViewModel model);
        Task<ResponseViewModel<LoginViewModel>> ForgotPassword(GetResetPasswordCodeViewModel model);

        /// user management  <summary>

        Task<ResponseViewModel<List<UserViewModel>>> GetAllUsers(UserViewModel model);

        Task<ResponseViewModel<UserViewModel>> UpdateUser(UserViewModel model);

        Task<ResponseViewModel<UserViewModel>> CreateUser(UserViewModel model);

        Task<ResponseViewModel<UserViewModel>> DeleteUser(UserViewModel model);

        Task<ResponseViewModel<UserViewModel>> GetUserInfo();

        Task<ResponseViewModel<UserViewModel>> GetUserInfoInTheLocalStorage();
        Task<ResponseViewModel<UserViewModel>> RemoveUserInfoInTheLocalStorage();

    }
}
