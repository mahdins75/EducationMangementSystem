using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Institution;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Implementation.Institution
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IApiService _apiService;
        private readonly string _baseUrl = "api/institution"; // This would be configured based on your API endpoints

        public InstitutionService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResponseViewModel<List<InstitutionViewModel>>> GetAll(InstitutionViewModel model)
        {
            var response = await _apiService.PostAsync<InstitutionViewModel, List<InstitutionViewModel>>($"{_baseUrl}/getall", model);
            return response;
        }

        public async Task<ResponseViewModel<InstitutionViewModel>> GetById(int id)
        {
            var response = await _apiService.GetAsync<InstitutionViewModel>($"{_baseUrl}/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionViewModel>>> Create(InstitutionViewModel model)
        {
            var response = await _apiService.PostAsync<InstitutionViewModel, List<InstitutionViewModel>>($"{_baseUrl}/create", model);
            return response;
        }

        public async Task<ResponseViewModel<InstitutionViewModel>> Update(InstitutionViewModel model)
        {
            var response = await _apiService.PutAsync<InstitutionViewModel, InstitutionViewModel>($"{_baseUrl}/update", model);
            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var response = await _apiService.DeleteAsync($"{_baseUrl}/delete/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionViewModel>>> SearchInstitutions(string searchTerm)
        {
            var response = await _apiService.GetAsync<List<InstitutionViewModel>>($"{_baseUrl}/search/{searchTerm}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionViewModel>>> GetActiveInstitutions()
        {
            var response = await _apiService.GetAsync<List<InstitutionViewModel>>($"{_baseUrl}/active");
            return response;
        }
    }
}