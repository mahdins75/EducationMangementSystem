using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.Membership
{
    public interface IRoleService
    {
        Task<ResponseViewModel<List<RoleViewModel>>> GetAll(RoleViewModel role);
        Task<ResponseViewModel<RoleViewModel>> Create(RoleViewModel role);
        Task<ResponseViewModel<RoleViewModel>> Edit(RoleViewModel role);
        Task<ResponseViewModel<RoleViewModel>> Delete(RoleViewModel role);
    }
}
