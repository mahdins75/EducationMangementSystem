using Mstech.Frontend.Wallet.ViewModel.DTO;

namespace Mstech.FrontEnd.Wallet.ViewModel.Membership
{
    public class UserRefreshTokenViewModel
    {
        public int Id { get; set; }
        public UserViewModel User { get; set; }
        public string Token { get; set; }


    }
}