using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
namespace Mstech.Frontend.Wallet.Service.Interface.WalletClient
{
    public interface IWalletClientService
    {
        Task<ResponseViewModel<List<WalletClientViewModel>>> GetAll(WalletClientViewModel model);
        Task<ResponseViewModel<WalletClientViewModel>> Create(WalletClientViewModel model);
        Task<ResponseViewModel<WalletClientViewModel>> Update(WalletClientViewModel model);
        Task<ResponseViewModel<bool>> Delete(WalletClientViewModel model);

    }
}
