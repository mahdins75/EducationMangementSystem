using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.WalletClient
{
    public interface IPaymentRequest
    {
        Task<ResponseViewModel<string>> CreateGatewayRequest(TransactionViewModel model);
    }
}