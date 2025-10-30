using Mstech.FrontEnd.Wallet.ViewModel;
using  Mstech.Frontend.Wallet.ViewModel.DTO;
namespace Mstech.Frontend.Wallet.Service.Interface.TransactionRequest
{
    public interface ITransactionRequestService
    {
        Task<ResponseViewModel<List<TransactionRequestViewModel>>> GetAll(TransactionRequestViewModel model);
        Task<ResponseViewModel<TransactionRequestViewModel>> GetById(int id);
        Task<ResponseViewModel<bool>> Approve(int id);
        Task<ResponseViewModel<Decimal>> SumOfAllRequestAmounts();
        Task<ResponseViewModel<TransactionRequestViewModel>> Create(TransactionRequestViewModel model);
    }
}