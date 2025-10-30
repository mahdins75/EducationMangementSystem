using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Membership;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using System.Reflection;

namespace Mstech.Frontend.Wallet.Service.Implementation.Membership
{
    public class UserService : IUserService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;
        public UserService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }

        public async Task<ResponseViewModel<ConfirmEmailNumberViewModel>> ConfirmRegistration(ConfirmEmailNumberViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/User/RegistrationConfirmation";
            var response = await apiService.PostAsync<ConfirmEmailNumberViewModel, ConfirmEmailNumberViewModel>(url, model);
            return response;
        }
        public async Task<ResponseViewModel<ConfirmPhoneNumberViewModel>> ConfirmPhoneNumber(ConfirmPhoneNumberViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/User/ConfirmPhoneNumber";

            var response = await apiService.PostAsync<ConfirmPhoneNumberViewModel, ConfirmPhoneNumberViewModel>(url, model);
            return response;
        }
        
        public async Task<ResponseViewModel<PhoneNumberConfirmationViewModel>> CreateConfirmationCodeForPhoneNumber(PhoneNumberConfirmationViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/Membership/CreateConfirmationCodeForPhoneNumber";

            var response = await apiService.PostAsync<PhoneNumberConfirmationViewModel, PhoneNumberConfirmationViewModel>(url, model);

            return response;
        }
        public async Task<ResponseViewModel<UserViewModel>> CreateUser(UserViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/User/Create";

            var response = await apiService.PostAsync<UserViewModel, UserViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<UserViewModel>> DeleteUser(UserViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/User/Delete";

            var response = await apiService.PostAsync<UserViewModel, UserViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<UserViewModel>> UpdateUser(UserViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/User/Edit";

            var response = await apiService.PostAsync<UserViewModel, UserViewModel>(url, model);

            return response;
        }

        public Task<ResponseViewModel<LoginViewModel>> ForgotPassword(GetResetPasswordCodeViewModel model)
        {

            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<LoginViewModel>> GeneratePasswordResetToken(GeneratePasswordResetTokenViewModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseViewModel<ConfirmPhoneNumberViewModel>> RegistrationConfirmationWithPhoneNumber(ConfirmPhoneNumberViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/User/RegistrationConfirmationWithPhoneNumber";

            var response = await apiService.PostAsync<ConfirmPhoneNumberViewModel, ConfirmPhoneNumberViewModel>(url, model);
            return response;
        }

        public async Task<ResponseViewModel<List<UserViewModel>>> GetAllUsers(UserViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Admin/User/GetAll?Id={model.Id}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}";

            var response = await apiService.GetAsync<List<UserViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<LoginViewModel>> Login(LoginViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/Membership/Login";

            var response = await apiService.PostAsync<LoginViewModel, LoginViewModel>(url, model);

            if (response.IsSuccess)
            {
                await localStorage.SetItemAsync("token", response.Entity.Token);
                await localStorage.SetItemAsync("refreshToken", response.Entity.RefreshToken);
            }
            return response;
        }

        public async Task<ResponseViewModel<bool>> Logout()
        {
            try
            {
                await localStorage.RemoveItemAsync("token");

                await localStorage.RemoveItemAsync("refreshToken");

                await localStorage.RemoveItemAsync("userInfo");

                return new ResponseViewModel<bool>() { IsSuccess = true, Entity = true };
            }
            catch
            {
                return new ResponseViewModel<bool>() { IsSuccess = false, Entity = false };
            }
        }

        public Task<ResponseViewModel<LoginViewModel>> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<RegisterViewModel>> Register(RegisterViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/Membership/Register";

            var response = await apiService.PostAsync<RegisterViewModel, RegisterViewModel>(url, model);

            return response;
        }

        public  async Task<ResponseViewModel<ResetPasswordViewModel>> ResetPassword(ResetPasswordViewModel model)
        {
           var url = generalInfo.GetApiBaseAddress() + "/api/Admin/Membership/ResetPassword";

            var response = await apiService.PostAsync<ResetPasswordViewModel, ResetPasswordViewModel>(url, model);
            return response;
        }

        public async Task<ResponseViewModel<UserViewModel>> GetUserInfo()
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/User/GetUserInfo";

            var response = await apiService.GetAsync<UserViewModel>(url);

            await localStorage.SetItemAsync("userInfo", response.Entity);

            return response;
        }
        public async Task<ResponseViewModel<UserViewModel>> CheckAUthentication()
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Common/Common/CheckAuthentication";

            var response = await apiService.GetAsync<UserViewModel>(url);

            return response;
        }

        public async Task<ResponseViewModel<UserViewModel>> GetUserInfoInTheLocalStorage()
        {
            var checkAuthentication = await CheckAUthentication();

            if (!checkAuthentication.IsSuccess)
            {
                return checkAuthentication;
            }

            var localValue = await localStorage.GetItemAsync<UserViewModel>("userInfo");

            return new ResponseViewModel<UserViewModel>() { Entity = localValue, IsSuccess = true };
        }

        public async Task<ResponseViewModel<UserViewModel>> RemoveUserInfoInTheLocalStorage()
        {
            await localStorage.RemoveItemAsync("userInfo");

            return new ResponseViewModel<UserViewModel>() { IsSuccess = true };
        }
    }
}
