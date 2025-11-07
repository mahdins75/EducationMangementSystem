using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Class;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Implementation.Class
{
    public class InstitutionClassService : IInstitutionClassService
    {
        private readonly IApiService _apiService;
        private readonly string _baseUrl = "api/institutionclass"; // This would be configured based on your API endpoints

        public InstitutionClassService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResponseViewModel<List<InstitutionClassViewModel>>> GetAll(InstitutionClassViewModel model)
        {
            var response = await _apiService.PostAsync<InstitutionClassViewModel, List<InstitutionClassViewModel>>($"{_baseUrl}/getall", model);
            return response;
        }

        public async Task<ResponseViewModel<InstitutionClassViewModel>> GetById(int id)
        {
            var response = await _apiService.GetAsync<InstitutionClassViewModel>($"{_baseUrl}/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionClassViewModel>>> Create(InstitutionClassViewModel model)
        {
            var response = await _apiService.PostAsync<InstitutionClassViewModel, List<InstitutionClassViewModel>>($"{_baseUrl}/create", model);
            return response;
        }

        public async Task<ResponseViewModel<InstitutionClassViewModel>> Update(InstitutionClassViewModel model)
        {
            var response = await _apiService.PutAsync<InstitutionClassViewModel, InstitutionClassViewModel>($"{_baseUrl}/update", model);
            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var response = await _apiService.DeleteAsync($"{_baseUrl}/delete/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionClassViewModel>>> SearchClasses(string searchTerm)
        {
            var response = await _apiService.GetAsync<List<InstitutionClassViewModel>>($"{_baseUrl}/search/{searchTerm}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionClassViewModel>>> GetByInstitutionId(int institutionId)
        {
            var response = await _apiService.GetAsync<List<InstitutionClassViewModel>>($"{_baseUrl}/byinstitution/{institutionId}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionClassViewModel>>> GetByTeacherId(string teacherId)
        {
            var response = await _apiService.GetAsync<List<InstitutionClassViewModel>>($"{_baseUrl}/byteacher/{teacherId}");
            return response;
        }
    }
}