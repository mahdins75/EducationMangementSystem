using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Implementation.WalletClient;
using Mstech.Frontend.Wallet.Service.Interface.WalletClient;

namespace Mstech.Frontend.Wallet.Service.Implementation.Wallet
{
    public class TransactionService : ITransactionService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;
        public TransactionService(IApiService apiService, GeneralInfo generalInfo, ILocalStorageService localStorage)
        {
            this.apiService = apiService;
            this.generalInfo = generalInfo;
            this.localStorage = localStorage;
        }
        public async Task<ResponseViewModel<List<TransactionViewModel>>> Create(TransactionViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/Transaction/Create";

            var response = await apiService.PostAsync<TransactionViewModel, List<TransactionViewModel>>(url, model);

            return response;
        }
        public async Task<ResponseViewModel<List<TransactionViewModel>>> CreateDepositForClientOwner(TransactionViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/Transaction/CreateDepositForClientOwner";

            var response = await apiService.PostAsync<TransactionViewModel, List<TransactionViewModel>>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<List<TransactionViewModel>>> Transfer(TransferTransactionViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/Transaction/Transfer";

            var response = await apiService.PostAsync<TransferTransactionViewModel, List<TransactionViewModel>>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<List<WithDrawConfirmationViewModel>>> WithDrawal(WithDrawConfirmationViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/Transaction/WithDrawal";

            var response = await apiService.PostAsync<WithDrawConfirmationViewModel, List<WithDrawConfirmationViewModel>>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(TransactionViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + "/api/Wallet/Transaction/Delete";

            var response = await apiService.PostAsync<TransactionViewModel, bool>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<List<TransactionViewModel>>> GetAll(TransactionViewModel model)
        {
            var clientApiId = model.Wallet != null ? model.Wallet.Client.ClientIdForApi : "";
            var WalletType = model.Wallet != null ? model.Wallet.WalletType : 0;

            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Transaction/GetAll?Id={model.Id}&WalletOwnerUserName={model.WalletOwnerUserName}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}&walletId={model.WalletId}&WalletType={WalletType}&InvoiceId={model.InvoiceId}&ClientApiId={clientApiId}";

            var response = await apiService.GetAsync<List<TransactionViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<List<TransactionInvoiceViewModel>>> GetAllInvoices(TransactionViewModel model)
        {
            string walletType = model.Wallet.WalletType != 0 ? model.Wallet.WalletType.ToString() : null;
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Transaction/GetAllInvoices?Id={model.Id}&WalletOwnerUserName={model.WalletOwnerUserName}&IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}&walletId={model.Id}&WalletType={walletType}";

            var response = await apiService.GetAsync<List<TransactionInvoiceViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<List<UserViewModel>>> GetAllOfMyUsers(TransactionViewModel model)
        {


            var url = generalInfo.GetApiBaseAddress() + $@"/api/Admin/User/GetAllUsersForMyClient?WaleltClientId={model.ClientApiId}&WalletType={WalletType.Purchase}";

            var response = await apiService.GetAsync<List<UserViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<List<TransactionViewModel>>> GetAllOfMyInvoices(GetAllOfMyInvoicesViewModel model)
        {

            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Transaction/GetAllOfMyInvoices?IsPagination={model.IsPagination}&PageSize={model.PageSize}&Skip={model.Skip}&ClientApiId={model.ClientId}&WalletOwnerUserName={model.WalletOwnerName}";

            var response = await apiService.GetAsync<List<TransactionViewModel>>(url);

            return response;
        }

        public async Task<ResponseViewModel<TransactionViewModel>> Update(TransactionViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Transaction/Update";

            var response = await apiService.PostAsync<TransactionViewModel, TransactionViewModel>(url, model);

            return response;
        }

        public async Task<ResponseViewModel<string>> GetBalance(WalletViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Wallet/Transaction/GetBalance?walletId={model.Id}";

            var response = await apiService.GetAsync<string>(url);

            return response;
        }
    }
}
