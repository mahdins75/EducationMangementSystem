using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.StudentActivity.Submission;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Implementation.StudentActivity.Submission
{
    public class StudentActivitySubmissionService : IStudentActivitySubmissionService
    {
        private readonly IApiService _apiService;
        private readonly string _baseUrl = "api/studentactivitysubmission"; // This would be configured based on your API endpoints

        public StudentActivitySubmissionService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetAll(StudentActivitySubmissionViewModel model)
        {
            var response = await _apiService.PostAsync<StudentActivitySubmissionViewModel, List<StudentActivitySubmissionViewModel>>($"{_baseUrl}/getall", model);
            return response;
        }

        public async Task<ResponseViewModel<StudentActivitySubmissionViewModel>> GetById(int id)
        {
            var response = await _apiService.GetAsync<StudentActivitySubmissionViewModel>($"{_baseUrl}/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> Create(StudentActivitySubmissionViewModel model)
        {
            var response = await _apiService.PostAsync<StudentActivitySubmissionViewModel, List<StudentActivitySubmissionViewModel>>($"{_baseUrl}/create", model);
            return response;
        }

        public async Task<ResponseViewModel<StudentActivitySubmissionViewModel>> Update(StudentActivitySubmissionViewModel model)
        {
            var response = await _apiService.PutAsync<StudentActivitySubmissionViewModel, StudentActivitySubmissionViewModel>($"{_baseUrl}/update", model);
            return response;
        }

        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var response = await _apiService.DeleteAsync($"{_baseUrl}/delete/{id}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> SearchSubmissions(string searchTerm)
        {
            var response = await _apiService.GetAsync<List<StudentActivitySubmissionViewModel>>($"{_baseUrl}/search/{searchTerm}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetByActivityId(int activityId)
        {
            var response = await _apiService.GetAsync<List<StudentActivitySubmissionViewModel>>($"{_baseUrl}/byactivity/{activityId}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetByStudentId(string studentId)
        {
            var response = await _apiService.GetAsync<List<StudentActivitySubmissionViewModel>>($"{_baseUrl}/bystudent/{studentId}");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetGradedSubmissions()
        {
            var response = await _apiService.GetAsync<List<StudentActivitySubmissionViewModel>>($"{_baseUrl}/graded");
            return response;
        }

        public async Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetUngradedSubmissions()
        {
            var response = await _apiService.GetAsync<List<StudentActivitySubmissionViewModel>>($"{_baseUrl}/ungraded");
            return response;
        }
    }
}