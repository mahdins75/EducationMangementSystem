using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Implementation;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using System.Collections.Generic;
using System.Reflection;

namespace Mstech.Frontend.Wallet.Service.Interface.Common
{
    public class SideNavService : ISideNavService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;
        public SideNavService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }
        public async Task<ResponseViewModel<List<NavItemViewModel>>> GetNavItems()
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Common/Common/GetNav";
            var response = await apiService.GetAsync<List<NavItemViewModel>>(url);
            return response;
        }
    }
}
