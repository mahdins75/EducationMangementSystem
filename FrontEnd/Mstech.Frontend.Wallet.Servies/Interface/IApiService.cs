using Mstech.FrontEnd.Wallet.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.Frontend.Wallet.Service.Interface
{
    public interface IApiService
    {
        Task<ResponseViewModel<TResponse>> GetAsync<TResponse>(string url);
        Task<ResponseViewModel<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest model);
        Task<ResponseViewModel<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest model);
        Task<ResponseViewModel<bool>> DeleteAsync(string url);
    }
}
