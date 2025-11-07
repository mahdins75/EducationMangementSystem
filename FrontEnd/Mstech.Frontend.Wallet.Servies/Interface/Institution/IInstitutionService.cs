using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.Institution
{
    public interface IInstitutionService
    {
        Task<ResponseViewModel<List<InstitutionViewModel>>> GetAll(InstitutionViewModel model);

        Task<ResponseViewModel<InstitutionViewModel>> GetById(int id);

        Task<ResponseViewModel<List<InstitutionViewModel>>> Create(InstitutionViewModel model);

        Task<ResponseViewModel<InstitutionViewModel>> Update(InstitutionViewModel model);

        Task<ResponseViewModel<bool>> Delete(int id);

        Task<ResponseViewModel<List<InstitutionViewModel>>> SearchInstitutions(string searchTerm);
        
        Task<ResponseViewModel<List<InstitutionViewModel>>> GetActiveInstitutions();
    }
}