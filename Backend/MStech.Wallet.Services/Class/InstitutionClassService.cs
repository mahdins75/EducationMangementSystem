using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using ViewModel.Infrastructure;
using MStech.Wallet.DataBase.Etity.Class;
using Mstech.Entity.Etity;
using Mstech.Accounting.Data;

namespace Implementation.ClassArea
{
    public class InstitutionClassService : BaseService<InstitutionClass>
    {
        private readonly ApplicationDbContext db;
        public InstitutionClassService(IUnitOfWork _unitOfWork, IRepository<InstitutionClass> _repositorys, ApplicationDbContext db) : base(_unitOfWork)
        {
            this.db = db;
        }

        public async Task<Result<List<InstitutionClassViewModel>>> GetAll(InstitutionClassViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Include(m => m.Institution)
                .Include(m => m.Teacher)
                .Where(x => !x.IsDeleted);

            var result = new Result<List<InstitutionClassViewModel>>();
            
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.SearchName))
            {
                query = query.Where(m => m.Name.Contains(model.SearchName) || m.ClassName.Contains(model.SearchName));
            }
            
            if (!string.IsNullOrEmpty(model.SearchClassCode))
            {
                query = query.Where(m => m.ClassCode.Contains(model.SearchClassCode));
            }
            
            if (model.InstitutionIdFilter.HasValue && model.InstitutionIdFilter > 0)
            {
                query = query.Where(m => m.InstitutionId == model.InstitutionIdFilter);
            }
            
            if (model.IsActiveFilter.HasValue)
            {
                query = query.Where(m => m.IsActive == model.IsActiveFilter.Value);
            }

            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new InstitutionClassViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    InstitutionId = m.InstitutionId,
                    ClassName = m.ClassName,
                    ClassCode = m.ClassCode,
                    MaxStudents = m.MaxStudents,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    IsActive = m.IsActive,
                    InstitutionName = m.Institution != null ? m.Institution.Name : null,
                    TeacherId = m.TeacherId,
                    TeacherFullName = m.Teacher != null ? m.Teacher.FullName : null,
                    TeacherUserName = m.Teacher != null ? m.Teacher.UserName : null,
                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new InstitutionClassViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    InstitutionId = m.InstitutionId,
                    ClassName = m.ClassName,
                    ClassCode = m.ClassCode,
                    MaxStudents = m.MaxStudents,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    IsActive = m.IsActive,
                    InstitutionName = m.Institution != null ? m.Institution.Name : null,
                    TeacherId = m.TeacherId,
                    TeacherFullName = m.Teacher != null ? m.Teacher.FullName : null,
                    TeacherUserName = m.Teacher != null ? m.Teacher.UserName : null,
                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
        }

        public async Task<Result<InstitutionClassViewModel>> CreateInstitutionClassAsync(InstitutionClassViewModel model)
        {
            InstitutionClass institutionClass = new InstitutionClass
            {
                Name = model.Name,
                Description = model.Description,
                InstitutionId = model.InstitutionId,
                ClassName = model.ClassName,
                ClassCode = model.ClassCode,
                MaxStudents = model.MaxStudents,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                IsActive = model.IsActive,
                IsDeleted = false,
                TeacherId = model.TeacherId
            };

            var result = this.Insert(institutionClass);

            model.Id = institutionClass.Id;

            return new Result<InstitutionClassViewModel>()
            {
                Message = "Class created successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<Result<InstitutionClassViewModel>> EditInstitutionClassAsync(InstitutionClassViewModel model)
        {
            var institutionClass = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
                
            if (institutionClass == null)
            {
                return new Result<InstitutionClassViewModel>()
                {
                    Message = "Class not found",
                    Success = false
                };
            }

            institutionClass.Name = model.Name;
            institutionClass.Description = model.Description;
            institutionClass.InstitutionId = model.InstitutionId;
            institutionClass.ClassName = model.ClassName;
            institutionClass.ClassCode = model.ClassCode;
            institutionClass.MaxStudents = model.MaxStudents;
            institutionClass.StartDate = model.StartDate;
            institutionClass.EndDate = model.EndDate;
            institutionClass.IsActive = model.IsActive;
            institutionClass.TeacherId = model.TeacherId;

            var result = this.Update(institutionClass);

            return new Result<InstitutionClassViewModel>()
            {
                Message = "Class updated successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<InstitutionClassViewModel> FindAsync(int id)
        {
            var institutionClass = await this.GetAllAsIqueriable()
                .Include(c => c.Institution)
                .Include(c => c.Teacher)
                .Select(m => new InstitutionClassViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    InstitutionId = m.InstitutionId,
                    ClassName = m.ClassName,
                    ClassCode = m.ClassCode,
                    MaxStudents = m.MaxStudents,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    IsActive = m.IsActive,
                    InstitutionName = m.Institution != null ? m.Institution.Name : null,
                    TeacherId = m.TeacherId,
                    TeacherFullName = m.Teacher != null ? m.Teacher.FullName : null,
                    TeacherUserName = m.Teacher != null ? m.Teacher.UserName : null,
                }).SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                
            return institutionClass;
        }

        public async Task<bool> DeleteInstitutionClassAsync(int id)
        {
            try
            {
                var institutionClass = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                if (institutionClass != null)
                {
                    institutionClass.IsDeleted = true;
                    this.Update(institutionClass);
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