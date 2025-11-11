using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Institution;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Implementation.Institution
{
    public class InstitutionDocumentService : IInstitutionDocumentService
    {
        private readonly IApiService _apiService;
        private readonly string _baseUrl = "api/institutiondocument"; // This would be configured based on your API endpoints

        public InstitutionDocumentService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResponseViewModel<List<InstitutionDocumentViewModel>>> GetAll(InstitutionDocumentViewModel model)
        {
            var response = await _apiService.PostAsync<InstitutionDocumentViewModel, List<InstitutionDocumentViewModel>>($"{_baseUrl}/getall", model);
            return response;
        }

        public async Task<ResponseViewModel<InstitutionDocumentViewModel>> GetById(int id)
        {
            var response = await _apiService.GetAsync<InstitutionDocumentViewModel>($"{_baseUrl}/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionDocumentViewModel>>> Create(CreateInstitutionDocumentViewModel model)
        {
            var response = await _apiService.PostAsync<CreateInstitutionDocumentViewModel, List<InstitutionDocumentViewModel>>($"{_baseUrl}/create", model);
            return response;
        }

        public async Task<ResponseViewModel<InstitutionDocumentViewModel>> Update(UpdateInstitutionDocumentViewModel model)
        {
            var response = await _apiService.PutAsync<UpdateInstitutionDocumentViewModel, InstitutionDocumentViewModel>($"{_baseUrl}/update", model);
            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var response = await _apiService.DeleteAsync($"{_baseUrl}/delete/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<InstitutionDocumentViewModel>>> SearchInstitutionDocuments(string searchTerm)
        {
            var response = await _apiService.GetAsync<List<InstitutionDocumentViewModel>>($"{_baseUrl}/search/{searchTerm}");
            return response;
        }
    }
}