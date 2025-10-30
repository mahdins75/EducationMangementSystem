using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Notification;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Implementation.Notification
{
    public class SMSService : ISMSService
    {
        private readonly ILocalStorageService localStorage;

        private readonly IApiService apiService;

        private readonly GeneralInfo generalInfo;

        public SMSService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }

        public async Task<ResponseViewModel<NeedToChangePasswordViewModel>> SendConfirmationSMS(NeedToChangePasswordViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Notification/SMS/SendConfirmationSMS";

            var response = await apiService.PostAsync<NeedToChangePasswordViewModel, NeedToChangePasswordViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<WithDrawConfirmationViewModel>> SendWithDrawalConfirmation(WithDrawConfirmationViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Notification/SMS/SendWithDrawalConfirmation";

            var response = await apiService.PostAsync<WithDrawConfirmationViewModel, WithDrawConfirmationViewModel>(url, model);

            return response;


        }
    }
}
