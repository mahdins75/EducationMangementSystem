using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using ViewModel.Infrastructure;
using MStech.Wallet.DataBase.Etity.Institution;
using Mstech.Entity.Etity;
using Mstech.Accounting.Data;

namespace Implementation.InstitutionArea
{
    public class InstitutionDocumentService : BaseService<InstitutionDocument>
    {
        private readonly ApplicationDbContext db;
        public InstitutionDocumentService(IUnitOfWork _unitOfWork, IRepository<InstitutionDocument> _repositorys, ApplicationDbContext db) : base(_unitOfWork)
        {
            this.db = db;
        }

        public async Task<Result<List<InstitutionDocumentViewModel>>> GetAll(InstitutionDocumentViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Include(m => m.Owner)
                .Include(m => m.InstitutionClass)
                .Where(x => !x.IsDeleted);

            var result = new Result<List<InstitutionDocumentViewModel>>();
            
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.SearchTitle))
            {
                // Since InstitutionDocument doesn't have a Title field, 
                // we can't search by title, but keep the field for consistency
            }
            
            if (model.InstitutionClassIdFilter.HasValue && model.InstitutionClassIdFilter > 0)
            {
                query = query.Where(m => m.InstitutionClassId == model.InstitutionClassIdFilter);
            }
            
            if (model.OwnerIdFilter != null && !string.IsNullOrEmpty(model.OwnerIdFilter.ToString()))
            {
                query = query.Where(m => m.OwnerId == model.OwnerIdFilter);
            }
            
            if (model.IsActiveFilter.HasValue)
            {
                query = query.Where(m => m.IsActive == model.IsActiveFilter.Value);
            }

            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new InstitutionDocumentViewModel()
                {
                    Id = m.Id,
                    IsActive = m.IsActive,
                    OwnerId = m.OwnerId,
                    OwnerName = m.Owner != null ? m.Owner.FullName : null,
                    OwnerUserName = m.Owner != null ? m.Owner.UserName : null,
                    InstitutionClassId = m.InstitutionClassId,
                    InstitutionClassName = m.InstitutionClass != null ? m.InstitutionClass.Name : null,
                    CreateDate = m.CreateDate,
                    ModifyDate = m.ModifyDate
                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new InstitutionDocumentViewModel()
                {
                    Id = m.Id,
                    IsActive = m.IsActive,
                    OwnerId = m.OwnerId,
                    OwnerName = m.Owner != null ? m.Owner.FullName : null,
                    OwnerUserName = m.Owner != null ? m.Owner.UserName : null,
                    InstitutionClassId = m.InstitutionClassId,
                    InstitutionClassName = m.InstitutionClass != null ? m.InstitutionClass.Name : null,
                    CreateDate = m.CreateDate,
                    ModifyDate = m.ModifyDate
                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
        }

        public async Task<Result<InstitutionDocumentViewModel>> CreateInstitutionDocumentAsync(InstitutionDocumentViewModel model)
        {
            InstitutionDocument document = new InstitutionDocument
            {
                IsActive = model.IsActive,
                OwnerId = model.OwnerId,
                InstitutionClassId = model.InstitutionClassId,
                IsDeleted = false
            };

            var result = this.Insert(document);

            model.Id = document.Id;

            return new Result<InstitutionDocumentViewModel>()
            {
                Message = "InstitutionDocument created successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<Result<InstitutionDocumentViewModel>> EditInstitutionDocumentAsync(InstitutionDocumentViewModel model)
        {
            var document = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
                
            if (document == null)
            {
                return new Result<InstitutionDocumentViewModel>()
                {
                    Message = "InstitutionDocument not found",
                    Success = false
                };
            }

            document.IsActive = model.IsActive;
            document.OwnerId = model.OwnerId;
            document.InstitutionClassId = model.InstitutionClassId;

            var result = this.Update(document);

            return new Result<InstitutionDocumentViewModel>()
            {
                Message = "InstitutionDocument updated successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<InstitutionDocumentViewModel> FindAsync(int id)
        {
            var document = await this.GetAllAsIqueriable()
                .Include(d => d.Owner)
                .Include(d => d.InstitutionClass)
                .Select(m => new InstitutionDocumentViewModel()
                {
                    Id = m.Id,
                    IsActive = m.IsActive,
                    OwnerId = m.OwnerId,
                    OwnerName = m.Owner != null ? m.Owner.FullName : null,
                    OwnerUserName = m.Owner != null ? m.Owner.UserName : null,
                    InstitutionClassId = m.InstitutionClassId,
                    InstitutionClassName = m.InstitutionClass != null ? m.InstitutionClass.Name : null,
                    CreateDate = m.CreateDate,
                    ModifyDate = m.ModifyDate
                }).SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                
            return document;
        }

        public async Task<bool> DeleteInstitutionDocumentAsync(int id)
        {
            try
            {
                var document = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                if (document != null)
                {
                    document.IsDeleted = true;
                    this.Update(document);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}