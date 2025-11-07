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
    public class InstitutionService : BaseService<Institution>
    {
        private readonly ApplicationDbContext db;
        public InstitutionService(IUnitOfWork _unitOfWork, IRepository<Institution> _repositorys, ApplicationDbContext db) : base(_unitOfWork)
        {
            this.db = db;
        }

        public async Task<Result<List<InstitutionViewModel>>> GetAll(InstitutionViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Classes).Include(m => m.Owner)
                .Where(x => !x.IsDeleted);

            var result = new Result<List<InstitutionViewModel>>();
            
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(m => m.Name.Contains(model.Name));
            }
            
            if (!string.IsNullOrEmpty(model.SearchName))
            {
                query = query.Where(m => m.Name.Contains(model.SearchName));
            }
            
            if (!string.IsNullOrEmpty(model.SearchEmail))
            {
                query = query.Where(m => m.Email.Contains(model.SearchEmail));
            }
            
            if (model.IsActiveFilter.HasValue)
            {
                query = query.Where(m => m.IsActive == model.IsActiveFilter.Value);
            }

            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new InstitutionViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Address = m.Address,
                    Phone = m.Phone,
                    Email = m.Email,
                    Website = m.Website,
                    IsActive = m.IsActive,
                    OwnerId = m.OwnerId,
                    OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                    OwnerUserName = m.Owner != null ? m.Owner.UserName : null,
                    ClassesCount = m.Classes != null ? m.Classes.Count() : 0
                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new InstitutionViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Address = m.Address,
                    Phone = m.Phone,
                    Email = m.Email,
                    Website = m.Website,
                    IsActive = m.IsActive,
                    OwnerId = m.OwnerId,
                    OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                    OwnerUserName = m.Owner != null ? m.Owner.UserName : null,
                    ClassesCount = m.Classes != null ? m.Classes.Count() : 0
                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
        }

        public async Task<Result<InstitutionViewModel>> CreateInstitutionAsync(InstitutionViewModel model)
        {
            Institution institution = new Institution
            {
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email,
                Website = model.Website,
                IsActive = model.IsActive,
                IsDeleted = false,
                OwnerId = model.OwnerId
            };

            var result = this.Insert(institution);

            model.Id = institution.Id;

            return new Result<InstitutionViewModel>()
            {
                Message = "Institution created successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<Result<InstitutionViewModel>> EditInstitutionAsync(InstitutionViewModel model)
        {
            var institution = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
                
            if (institution == null)
            {
                return new Result<InstitutionViewModel>()
                {
                    Message = "Institution not found",
                    Success = false
                };
            }

            institution.Name = model.Name;
            institution.Description = model.Description;
            institution.Address = model.Address;
            institution.Phone = model.Phone;
            institution.Email = model.Email;
            institution.Website = model.Website;
            institution.IsActive = model.IsActive;
            institution.OwnerId = model.OwnerId;

            var result = this.Update(institution);

            return new Result<InstitutionViewModel>()
            {
                Message = "Institution updated successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<InstitutionViewModel> FindAsync(int id)
        {
            var institution = await this.GetAllAsIqueriable()
                .Include(i => i.Owner)
                .Include(i => i.Classes)
                .Select(m => new InstitutionViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Address = m.Address,
                    Phone = m.Phone,
                    Email = m.Email,
                    Website = m.Website,
                    IsActive = m.IsActive,
                    IsDeleted = m.IsDeleted,
                    OwnerId = m.OwnerId,
                    OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                    OwnerUserName = m.Owner != null ? m.Owner.UserName : null,
                    ClassesCount = m.Classes != null ? m.Classes.Count() : 0
                }).SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                
            return institution;
        }

        public async Task<bool> DeleteInstitutionAsync(int id)
        {
            try
            {
                var institution = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                if (institution != null)
                {
                    institution.IsDeleted = true;
                    this.Update(institution);
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