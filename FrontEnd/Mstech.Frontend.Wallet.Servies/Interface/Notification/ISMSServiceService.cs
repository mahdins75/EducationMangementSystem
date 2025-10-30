using Mstech.Frontend.Wallet.ViewModel.DTO;

using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.Notification
{
    public interface ISMSService
    {
        Task<ResponseViewModel<NeedToChangePasswordViewModel>> SendConfirmationSMS(NeedToChangePasswordViewModel model);
        Task<ResponseViewModel<WithDrawConfirmationViewModel>> SendWithDrawalConfirmation(WithDrawConfirmationViewModel model);

    }
}
