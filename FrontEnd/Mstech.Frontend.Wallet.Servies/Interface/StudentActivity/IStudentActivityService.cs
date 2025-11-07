using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.StudentActivity
{
    public interface IStudentActivityService
    {
        Task<ResponseViewModel<List<StudentActivityViewModel>>> GetAll(StudentActivityViewModel model);

        Task<ResponseViewModel<List<StudentActivityViewModel>>> GetByStudentId(string studentId);

        Task<ResponseViewModel<List<StudentActivityViewModel>>> GetByActivityType(string activityType);

        Task<ResponseViewModel<StudentActivityViewModel>> GetById(int id);

        Task<ResponseViewModel<List<StudentActivityViewModel>>> Create(CreateStudentActivityViewModel model);

        Task<ResponseViewModel<StudentActivityViewModel>> Update(UpdateStudentActivityViewModel model);

        Task<ResponseViewModel<bool>> Delete(int id);

        Task<ResponseViewModel<List<StudentActivityViewModel>>> SearchActivities(string searchTerm);
    }
}