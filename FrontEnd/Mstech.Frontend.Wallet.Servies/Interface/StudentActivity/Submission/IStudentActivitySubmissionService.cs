using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.StudentActivity.Submission
{
    public interface IStudentActivitySubmissionService
    {
        Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetAll(StudentActivitySubmissionViewModel model);

        Task<ResponseViewModel<StudentActivitySubmissionViewModel>> GetById(int id);

        Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> Create(StudentActivitySubmissionViewModel model);

        Task<ResponseViewModel<StudentActivitySubmissionViewModel>> Update(StudentActivitySubmissionViewModel model);

        Task<ResponseViewModel<bool>> Delete(int id);

        Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> SearchSubmissions(string searchTerm);
        
        Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetByActivityId(int activityId);
        
        Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetByStudentId(string studentId);
        
        Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetGradedSubmissions();
        
        Task<ResponseViewModel<List<StudentActivitySubmissionViewModel>>> GetUngradedSubmissions();
    }
}