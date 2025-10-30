using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Interface.TransactionRequest;
using Mstech.Frontend.wallet.ViewModel.Wallet;

namespace Mstech.Frontend.Wallet.Service.Implementation.TransactionRequest
{
    public class TransactionRequestService : ITransactionRequestService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;

        public TransactionRequestService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }

        public async Task<ResponseViewModel<List<TransactionRequestViewModel>>> GetAll(TransactionRequestViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/TransactionRequest/GetAll?Id={model.Id}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}";

            var response = await apiService.GetAsync<List<TransactionRequestViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<Decimal>> SumOfAllRequestAmounts()
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/TransactionRequest/SumOfAllRequestAmounts";

            var response = await apiService.GetAsync<Decimal>(url);

            return response;
        }

        public async Task<ResponseViewModel<TransactionRequestViewModel>> GetById(int id)
        {
            var url = generalInfo.GetApiBaseAddress() + $"/api/TransactionRequest/GetById/{id}";

            var response = await apiService.GetAsync<TransactionRequestViewModel>(url);

            return response;
        }

        public async Task<ResponseViewModel<bool>> Approve(int id)
        {
            var url = generalInfo.GetApiBaseAddress() + $"/api/TransactionRequest/Approve/{id}";

            var response = await apiService.PostAsync<object, bool>(url, null);

            return response;
        }

        public async Task<ResponseViewModel<bool>> Reject(int id)
        {
            var url = generalInfo.GetApiBaseAddress() + $"/api/TransactionRequest/Reject/{id}";

            var response = await apiService.PostAsync<object, bool>(url, null);

            return response;
        }

        public async Task<ResponseViewModel<TransactionRequestViewModel>> Create(TransactionRequestViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/TransactionRequest/Create";

            var response = await apiService.PostAsync<TransactionRequestViewModel, TransactionRequestViewModel>(url, model);

            return response;
        }
    }
}