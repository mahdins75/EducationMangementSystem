using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.Frontend.Wallet.ViewModel.ViewModel.Dashboard;
using Mstech.FrontEnd.Wallet.ViewModel;
namespace Mstech.Frontend.Wallet.Service.Implementation.WalletClient
{
    public interface IDashboardService
    {
        Task<ResponseViewModel<DashboarAggrigation>> GetAll();

    }
}
