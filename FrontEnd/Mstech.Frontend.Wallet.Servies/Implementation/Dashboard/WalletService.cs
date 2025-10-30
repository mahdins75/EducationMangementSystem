using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Implementation.WalletClient;
using Mstech.Frontend.Wallet.ViewModel.ViewModel.Dashboard;

namespace Mstech.Frontend.Wallet.Service.Implementation.Wallet
{
    public class DashboardService : IDashboardService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;
        public DashboardService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }

        public async Task<ResponseViewModel<DashboarAggrigation>> GetAll()
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Admin/DashBoard/GetDasBoardInfo";

            var response = await apiService.GetAsync<DashboarAggrigation>(url);

            return response;
        }


    }
}
