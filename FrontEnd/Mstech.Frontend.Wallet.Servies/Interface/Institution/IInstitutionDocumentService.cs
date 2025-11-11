using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Interface.Institution
{
    public interface IInstitutionDocumentService
    {
        Task<ResponseViewModel<List<InstitutionDocumentViewModel>>> GetAll(InstitutionDocumentViewModel model);

        Task<ResponseViewModel<InstitutionDocumentViewModel>> GetById(int id);

        Task<ResponseViewModel<List<InstitutionDocumentViewModel>>> Create(CreateInstitutionDocumentViewModel model);

        Task<ResponseViewModel<InstitutionDocumentViewModel>> Update(UpdateInstitutionDocumentViewModel model);

        Task<ResponseViewModel<bool>> Delete(int id);

        Task<ResponseViewModel<List<InstitutionDocumentViewModel>>> SearchInstitutionDocuments(string searchTerm);
    }
}