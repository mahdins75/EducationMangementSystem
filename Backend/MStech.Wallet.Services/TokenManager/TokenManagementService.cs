using Microsoft.AspNetCore.Identity;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;

namespace Implementation.TokenManagement
{

    public class TokenService
    {
        private List<UserRefreshTokenViewModel> _refreshTokenView = new List<UserRefreshTokenViewModel>();
        public TokenService()
        {
        }
        public async Task<UserViewModel> GetUserFromRefreshToken(string refreshToken)
        {
            try
            {
                var userTermp = _refreshTokenView.Where(m => m.Token == refreshToken).FirstOrDefault();
                return userTermp == null ? null : userTermp.User;
            }
            catch(Exception)
            {
                return null;
            }

        }

        public async Task<bool> AddRefreshToken(string refreshToken, UserViewModel user)
        {
            try
            {
                _refreshTokenView.Add(new UserRefreshTokenViewModel() { User = user, Token = refreshToken });
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RemoveRefreshToken(UserViewModel user)
        {
            try
            {
                _refreshTokenView.Remove(_refreshTokenView.Where(m => m.User == user).FirstOrDefault());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
