using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Membership;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;

namespace Mstech.Frontend.Wallet.Service.Implementation.Membership
{
    public class RoleService : IRoleService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;
        public RoleService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }

        public async Task<ResponseViewModel<RoleViewModel>> Create(RoleViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/Role/GetAll";

            var response = await apiService.PostAsync<RoleViewModel, RoleViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<RoleViewModel>> Delete(RoleViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/Role/Delete";

            var response = await apiService.PostAsync<RoleViewModel, RoleViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<RoleViewModel>> Edit(RoleViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/Role/Edit";

            var response = await apiService.PostAsync<RoleViewModel, RoleViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<List<RoleViewModel>>> GetAll(RoleViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/Role/GetAll";

            var response = await apiService.GetAsync<List<RoleViewModel>>(url);

            return response;
        }
    }
}
