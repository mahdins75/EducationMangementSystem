using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.StudentActivity;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Implementation.StudentActivity
{
    public class StudentActivityService : IStudentActivityService
    {
        private readonly IApiService _apiService;
        private readonly string _baseUrl = "api/studentactivity"; // This would be configured based on your API endpoints

        public StudentActivityService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResponseViewModel<List<StudentActivityViewModel>>> GetAll(StudentActivityViewModel model)
        {
            var response = await _apiService.PostAsync<StudentActivityViewModel, List<StudentActivityViewModel>>($"{_baseUrl}/getall", model);
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivityViewModel>>> GetByStudentId(string studentId)
        {
            var response = await _apiService.GetAsync<List<StudentActivityViewModel>>($"{_baseUrl}/bystudent/{studentId}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivityViewModel>>> GetByActivityType(string activityType)
        {
            var response = await _apiService.GetAsync<List<StudentActivityViewModel>>($"{_baseUrl}/bytype/{activityType}");
            return response;
        }

        public async Task<ResponseViewModel<StudentActivityViewModel>> GetById(int id)
        {
            var response = await _apiService.GetAsync<StudentActivityViewModel>($"{_baseUrl}/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivityViewModel>>> Create(CreateStudentActivityViewModel model)
        {
            var response = await _apiService.PostAsync<CreateStudentActivityViewModel, List<StudentActivityViewModel>>($"{_baseUrl}/create", model);
            return response;
        }

        public async Task<ResponseViewModel<StudentActivityViewModel>> Update(UpdateStudentActivityViewModel model)
        {
            var response = await _apiService.PutAsync<UpdateStudentActivityViewModel, StudentActivityViewModel>($"{_baseUrl}/update", model);
            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var response = await _apiService.DeleteAsync($"{_baseUrl}/delete/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivityViewModel>>> SearchActivities(string searchTerm)
        {
            var response = await _apiService.GetAsync<List<StudentActivityViewModel>>($"{_baseUrl}/search/{searchTerm}");
            return response;
        }
    }
}