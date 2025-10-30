using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Membership;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.FrontEnd.Wallet.ViewModel.Membership;
using Mstech.Frontend.Wallet.ViewModel.DTO;

namespace Mstech.Frontend.Wallet.Service.Implementation.Membership
{
    public class AccessedLinkService : IAccessedLinkService
    {
        private readonly ILocalStorageService localStorage;

        private readonly IApiService apiService;

        private readonly GeneralInfo generalInfo;
        public AccessedLinkService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }

        public async Task<ResponseViewModel<AccessedLinkViewModel>> Create(AccessedLinkViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/AccessedLink/GetAll";

            var response = await apiService.PostAsync<AccessedLinkViewModel, AccessedLinkViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<AccessedLinkViewModel>> Delete(AccessedLinkViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/AccessedLink/Delete";

            var response = await apiService.PostAsync<AccessedLinkViewModel, AccessedLinkViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<AccessedLinkViewModel>> Edit(AccessedLinkViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/AccessedLink/Edit";

            var response = await apiService.PostAsync<AccessedLinkViewModel, AccessedLinkViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<List<AccessedLinkViewModel>>> GetAll(AccessedLinkViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Admin/AccessedLink/GetAll";

            var response = await apiService.GetAsync<List<AccessedLinkViewModel>>(url);

            return response;
        }
    }
}
