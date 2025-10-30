using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.WalletClient
{
    public interface ITransactionService
    {
        Task<ResponseViewModel<List<TransactionViewModel>>> GetAll(TransactionViewModel model);
        Task<ResponseViewModel<List<TransactionViewModel>>> CreateDepositForClientOwner(TransactionViewModel model);
        Task<ResponseViewModel<List<TransactionViewModel>>> GetAllOfMyInvoices(GetAllOfMyInvoicesViewModel model);
        Task<ResponseViewModel<string>> GetBalance(WalletViewModel model);
        Task<ResponseViewModel<List<TransactionInvoiceViewModel>>> GetAllInvoices(TransactionViewModel model);
        Task<ResponseViewModel<List<UserViewModel>>> GetAllOfMyUsers(TransactionViewModel model);
        Task<ResponseViewModel<List<TransactionViewModel>>> Create(TransactionViewModel model);
        Task<ResponseViewModel<List<TransactionViewModel>>> Transfer(TransferTransactionViewModel model);
        Task<ResponseViewModel<List<WithDrawConfirmationViewModel>>> WithDrawal(WithDrawConfirmationViewModel model);
        Task<ResponseViewModel<TransactionViewModel>> Update(TransactionViewModel model);
        Task<ResponseViewModel<bool>> Delete(TransactionViewModel model);
    }
}