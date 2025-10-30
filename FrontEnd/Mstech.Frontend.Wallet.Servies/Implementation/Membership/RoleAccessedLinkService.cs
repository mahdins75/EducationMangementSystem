using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Membership;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.FrontEnd.Wallet.ViewModel.Membership;

namespace Mstech.Frontend.Wallet.Service.Implementation.Membership
{
    public class RoleAccessedLinkService : IRoleAccessedLinkService
    {
        private readonly ILocalStorageService localStorage;

        private readonly IApiService apiService;

        private readonly GeneralInfo generalInfo;
        public RoleAccessedLinkService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }

        public async Task<ResponseViewModel<List<RoleAccessedLinkViewModel>>> Create(CreateRoleAccessLinkViewmodel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/RoleAccessedLink/Create";

            var response = await apiService.PostAsync<CreateRoleAccessLinkViewmodel, List<RoleAccessedLinkViewModel>>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<RoleAccessedLinkViewModel>> Delete(RoleAccessedLinkViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/RoleAccessedLink/Delete";

            var response = await apiService.PostAsync<RoleAccessedLinkViewModel, RoleAccessedLinkViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<RoleAccessedLinkViewModel>> Edit(RoleAccessedLinkViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/RoleAccessedLink/Edit";

            var response = await apiService.PostAsync<RoleAccessedLinkViewModel, RoleAccessedLinkViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<List<RoleAccessedLinkViewModel>>> GetAll(RoleAccessedLinkViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Membership/RoleAccessedLink/GetAll?RoleId=" + model.RoleId;

            var response = await apiService.GetAsync<List<RoleAccessedLinkViewModel>>(url);

            return response;
        }
    }
}
