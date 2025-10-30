using Mstech.FrontEnd.Wallet.ViewModel;
using Mstech.FrontEnd.Wallet.ViewModel.Membership;

namespace Mstech.Frontend.Wallet.Service.Interface.Membership
{
    public interface IRoleAccessedLinkService
    {
        Task<ResponseViewModel<List<RoleAccessedLinkViewModel>>> GetAll(RoleAccessedLinkViewModel role);
        Task<ResponseViewModel<List<RoleAccessedLinkViewModel>>> Create(CreateRoleAccessLinkViewmodel model);
        Task<ResponseViewModel<RoleAccessedLinkViewModel>> Edit(RoleAccessedLinkViewModel role);
        Task<ResponseViewModel<RoleAccessedLinkViewModel>> Delete(RoleAccessedLinkViewModel role);
    }
}
