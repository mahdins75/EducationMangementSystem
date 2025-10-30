using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Implementation.WalletClient;
using Mstech.Frontend.Wallet.Service.Interface.WalletClient;

namespace Mstech.Frontend.Wallet.Service.Implementation.Wallet
{
    public class WalletService : IWalletService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;
        public WalletService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }
        public async Task<ResponseViewModel<List<WalletViewModel>>> Create(WalletViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/Wallet/Create";

            var response = await apiService.PostAsync<WalletViewModel, List<WalletViewModel>>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(WalletViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/Wallet/Delete";

            var response = await apiService.PostAsync<WalletViewModel, bool>(url, model);

            return response;
        }


        public async Task<ResponseViewModel<List<WalletViewModel>>> GetAll(WalletViewModel model)
        {
            var userName = model.User != null ? model.User.UserName : "";

            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Wallet/GetAll?Id={model.Id}&UserName={userName}&WalletType={model.WalletType}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}";

            var response = await apiService.GetAsync<List<WalletViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<List<WalletViewModel>>> GetAllForClientOwner(WalletViewModel model)
        {
            var userName = model.User != null ? model.User.UserName : "";

            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Wallet/GetAllForClientOwner?Id={model.Id}&UserName={userName}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}&ClientId={model.ClientId}&WalletType={model.WalletType}";

            var response = await apiService.GetAsync<List<WalletViewModel>>(url);

            return response;
        }
        public async Task<ResponseViewModel<List<WalletViewModel>>> GetAllForCurrentUser(WalletViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Wallet/GetAllForCurrentUser?Id={model.Id}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}&WalletType={model.WalletType}";

            var response = await apiService.GetAsync<List<WalletViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<WalletViewModel>> Update(WalletViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Wallet/Update";

            var response = await apiService.PostAsync<WalletViewModel, WalletViewModel>(url, model);

            return response;
        }
    }
}
