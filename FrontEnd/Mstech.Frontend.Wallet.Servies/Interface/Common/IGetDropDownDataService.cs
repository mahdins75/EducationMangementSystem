using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
namespace Mstech.Frontend.Wallet.Service.Interface.Common
{
    public interface IGetDropDownDataService
    {
        Task<ResponseViewModel<List<SelectListItemStringValue>>> GetDropDownUsers(UserViewModel model);
        Task<ResponseViewModel<List<SelectListItem>>> GetEnumFropDown<TEnum>() where TEnum : Enum;
       

    }
}
