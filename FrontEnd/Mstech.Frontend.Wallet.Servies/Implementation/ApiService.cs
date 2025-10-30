using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.FrontEnd.Wallet.ViewModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Mstech.Frontend.Wallet.Service.Implementation
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly string _clientApi;

        public ApiService(HttpClient httpClient, ILocalStorageService localStorage, string clientApi)
        {
            _httpClient = httpClient;
            this.localStorage = localStorage;
            _clientApi = clientApi;
        }

        public async Task<ResponseViewModel<TResponse>> GetAsync<TResponse>(string url)
        {
            var token = await localStorage.GetItemAsync<string>("token");
            var refreshToken = await localStorage.GetItemAsync<string>("refreshToken");

            if (!string.IsNullOrEmpty(token))
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            }
            if (!string.IsNullOrEmpty(refreshToken))
            {
                if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "X-Refresh-Token"))
                {
                    var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "X-Refresh-Token");
                    foreach (var item in temp)
                    {
                        _httpClient.DefaultRequestHeaders.Remove(item.Key);
                    }
                }
            }
            _httpClient.DefaultRequestHeaders.Add("X-Refresh-Token", refreshToken);

            if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "clientidforapis"))
            {
                var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "clientidforapis");
                foreach (var item in temp)
                {
                    _httpClient.DefaultRequestHeaders.Remove(item.Key);
                }
            }
            _httpClient.DefaultRequestHeaders.Add("clientidforapis", _clientApi);

            
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ResponseViewModel<TResponse>
                {
                    Message = "دسترسی غیرمجاز. لطفاً وارد شوید.",
                    IsSuccess = false,
                    ErrorCode = response.StatusCode.ToString()
                };
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResponseViewModel<TResponse>>();
        }

        public async Task<ResponseViewModel<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest model)
        {
            var token = await localStorage.GetItemAsync<string>("token");
            var refreshToken = await localStorage.GetItemAsync<string>("refreshToken");

            if (!string.IsNullOrEmpty(token))
            {

                _httpClient.DefaultRequestHeaders.Authorization = null;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            }
            if (!string.IsNullOrEmpty(refreshToken))
            {
                if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "X-Refresh-Token"))
                {
                    var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "X-Refresh-Token");
                    foreach (var item in temp)
                    {
                        _httpClient.DefaultRequestHeaders.Remove(item.Key);
                    }
                }
                _httpClient.DefaultRequestHeaders.Add("X-Refresh-Token", refreshToken);
            }
            if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "clientidforapis"))
            {
                var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "clientidforapis");
                foreach (var item in temp)
                {
                    _httpClient.DefaultRequestHeaders.Remove(item.Key);
                }
            }
            _httpClient.DefaultRequestHeaders.Add("clientidforapis", _clientApi);

            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ResponseViewModel<TResponse>
                {
                    Message = "دسترسی غیرمجاز. لطفاً وارد شوید.",
                    IsSuccess = false,
                    ErrorCode = response.StatusCode.ToString()
                };
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResponseViewModel<TResponse>>();
        }

        public async Task<ResponseViewModel<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest model)
        {
            var token = await localStorage.GetItemAsync<string>("token");
            var refreshToken = await localStorage.GetItemAsync<string>("refreshToken");

            if (!string.IsNullOrEmpty(token))
            {

                _httpClient.DefaultRequestHeaders.Authorization = null;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            }
            if (!string.IsNullOrEmpty(refreshToken))
            {
                if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "X-Refresh-Token"))
                {
                    var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "X-Refresh-Token");
                    foreach (var item in temp)
                    {
                        _httpClient.DefaultRequestHeaders.Remove(item.Key);
                    }
                }
                _httpClient.DefaultRequestHeaders.Add("X-Refresh-Token", refreshToken);
            }
            if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "clientidforapis"))
            {
                var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "clientidforapis");
                foreach (var item in temp)
                {
                    _httpClient.DefaultRequestHeaders.Remove(item.Key);
                }
            }
            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ResponseViewModel<TResponse>
                {
                    Message = "دسترسی غیرمجاز. لطفاً وارد شوید.",
                    IsSuccess = false,
                    ErrorCode = response.StatusCode.ToString()
                };
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResponseViewModel<TResponse>>();
        }

        public async Task<ResponseViewModel<bool>> DeleteAsync(string url)
        {
            var token = await localStorage.GetItemAsync<string>("token");
            var refreshToken = await localStorage.GetItemAsync<string>("refreshToken");

            if (!string.IsNullOrEmpty(token))
            {

                _httpClient.DefaultRequestHeaders.Authorization = null;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            }
            if (!string.IsNullOrEmpty(refreshToken))
            {
                if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "X-Refresh-Token"))
                {
                    var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "X-Refresh-Token");
                    foreach (var item in temp)
                    {
                        _httpClient.DefaultRequestHeaders.Remove(item.Key);
                    }
                }
                _httpClient.DefaultRequestHeaders.Add("X-Refresh-Token", refreshToken);
            }
            if (_httpClient.DefaultRequestHeaders.Any(m => m.Key == "clientidforapis"))
            {
                var temp = _httpClient.DefaultRequestHeaders.Where(m => m.Key == "clientidforapis");
                foreach (var item in temp)
                {
                    _httpClient.DefaultRequestHeaders.Remove(item.Key);
                }
            }
            var response = await _httpClient.DeleteAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ResponseViewModel<bool>
                {
                    Message = "دسترسی غیرمجاز. لطفاً وارد شوید.",
                    IsSuccess = false,
                    ErrorCode = response.StatusCode.ToString()
                };
            }
            response.EnsureSuccessStatusCode();
            return new ResponseViewModel<bool>()
            {
                Message = "",
                IsSuccess = true,
                ErrorCode = response.StatusCode.ToString()
            };
        }
    }
}
