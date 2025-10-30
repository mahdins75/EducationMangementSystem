using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
namespace Mstech.Frontend.Wallet.Service.Interface.WalletClient
{
    public interface IWalletService
    {
        Task<ResponseViewModel<List<WalletViewModel>>> GetAll(WalletViewModel model);

        Task<ResponseViewModel<List<WalletViewModel>>> GetAllForClientOwner(WalletViewModel model);

        Task<ResponseViewModel<List<WalletViewModel>>> GetAllForCurrentUser(WalletViewModel model);

        Task<ResponseViewModel<List<WalletViewModel>>> Create(WalletViewModel model);

        Task<ResponseViewModel<WalletViewModel>> Update(WalletViewModel model);

        Task<ResponseViewModel<bool>> Delete(WalletViewModel model);

    }
}
