using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.Membership
{
    public interface IAccessedLinkService
    {
        Task<ResponseViewModel<List<AccessedLinkViewModel>>> GetAll(AccessedLinkViewModel role);
        Task<ResponseViewModel<AccessedLinkViewModel>> Create(AccessedLinkViewModel role);
        Task<ResponseViewModel<AccessedLinkViewModel>> Edit(AccessedLinkViewModel role);
        Task<ResponseViewModel<AccessedLinkViewModel>> Delete(AccessedLinkViewModel role);
    }
}
