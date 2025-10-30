using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
namespace Mstech.Frontend.Wallet.Service.Interface.Common
{
    public interface IConfigInfoService
    {
        Task<string> GetCLientId();
        Task<bool> SetCLientId(string id);
    }
}
