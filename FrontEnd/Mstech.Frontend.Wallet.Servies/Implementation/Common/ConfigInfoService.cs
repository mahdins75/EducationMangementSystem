using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Implementation;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using System.Collections.Generic;
using System.Reflection;

namespace Mstech.Frontend.Wallet.Service.Interface.Common
{
    public class ConfigInfoService : IConfigInfoService
    {
        private string clientId;
        public ConfigInfoService()
        {
        }
        public async Task<string> GetCLientId()
        {
            return clientId;
        }
        public async Task<bool> SetCLientId(string id)
        {
            clientId = id;
            return true;
        }
    }
}
