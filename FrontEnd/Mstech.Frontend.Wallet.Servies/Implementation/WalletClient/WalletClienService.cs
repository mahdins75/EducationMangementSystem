using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Interface.WalletClient;

namespace Mstech.Frontend.Wallet.Service.Implementation.WalletClient
{
    public class WalletClientService : IWalletClientService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;
        public WalletClientService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }
        public async Task<ResponseViewModel<WalletClientViewModel>> Create(WalletClientViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/WalletClient/Create";

            var response = await apiService.PostAsync<WalletClientViewModel, WalletClientViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(WalletClientViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/WalletClient/Delete";

            var response = await apiService.PostAsync<WalletClientViewModel, bool>(url, model);

            return response;
        }


        public async Task<ResponseViewModel<List<WalletClientViewModel>>> GetAll(WalletClientViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/WalletClient/GetAll?Id={model.Id}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}&UserName={model.UserName}";

            var response = await apiService.GetAsync<List<WalletClientViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<WalletClientViewModel>> Update(WalletClientViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/WalletClient/Update";

            var response = await apiService.PostAsync<WalletClientViewModel, WalletClientViewModel>(url, model);

            return response;
        }
    }
}
