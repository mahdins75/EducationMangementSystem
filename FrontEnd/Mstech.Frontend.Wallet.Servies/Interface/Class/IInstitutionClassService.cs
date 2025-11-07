using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.Class
{
    public interface IInstitutionClassService
    {
        Task<ResponseViewModel<List<InstitutionClassViewModel>>> GetAll(InstitutionClassViewModel model);

        Task<ResponseViewModel<InstitutionClassViewModel>> GetById(int id);

        Task<ResponseViewModel<List<InstitutionClassViewModel>>> Create(InstitutionClassViewModel model);

        Task<ResponseViewModel<InstitutionClassViewModel>> Update(InstitutionClassViewModel model);

        Task<ResponseViewModel<bool>> Delete(int id);

        Task<ResponseViewModel<List<InstitutionClassViewModel>>> SearchClasses(string searchTerm);
        
        Task<ResponseViewModel<List<InstitutionClassViewModel>>> GetByInstitutionId(int institutionId);
        
        Task<ResponseViewModel<List<InstitutionClassViewModel>>> GetByTeacherId(string teacherId);
    }
}