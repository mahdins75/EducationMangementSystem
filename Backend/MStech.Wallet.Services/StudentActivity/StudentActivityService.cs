using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using ViewModel.Infrastructure;
using MStech.Wallet.DataBase.Etity.StudentActivity;
using Mstech.Accounting.Data;

namespace Implementation.StudentActivityArea
{
    public class StudentActivityService : BaseService<StudentActivity>
    {
        private readonly ApplicationDbContext db;
        public StudentActivityService(IUnitOfWork _unitOfWork, IRepository<StudentActivity> _repositorys, ApplicationDbContext db) : base(_unitOfWork)
        {
            this.db = db;
        }

        public async Task<Result<List<StudentActivityViewModel>>> GetAll(StudentActivityViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Include(m => m.Class)
                .Include(m => m.CreatedBy)
                .Include(m => m.Student)  // Include Student relationship
                .Include(m => m.Submissions)
                .Where(x => !x.IsDeleted);

            var result = new Result<List<StudentActivityViewModel>>();
            
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.SearchTitle))
            {
                query = query.Where(m => m.Title.Contains(model.SearchTitle));
            }
            
            if (!string.IsNullOrEmpty(model.ActivityTypeFilter))
            {
                query = query.Where(m => m.ActivityType == model.ActivityTypeFilter);
            }
            
            if (model.ClassIdFilter.HasValue && model.ClassIdFilter > 0)
            {
                query = query.Where(m => m.ClassId == model.ClassIdFilter);
            }
            
            if (model.IsActiveFilter.HasValue)
            {
                query = query.Where(m => m.IsActive == model.IsActiveFilter.Value);
            }
            
            if (model.StartDateFrom.HasValue)
            {
                query = query.Where(m => m.StartDate >= model.StartDateFrom.Value);
            }
            
            if (model.DueDateTo.HasValue)
            {
                query = query.Where(m => m.DueDate <= model.DueDateTo.Value);
            }
            
            if (!string.IsNullOrEmpty(model.StudentId))
            {
                query = query.Where(m => m.StudentId == model.StudentId);
            }

            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new StudentActivityViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ClassId = m.ClassId,
                    ClassName = m.Class != null ? m.Class.Name : null,
                    ActivityType = m.ActivityType,
                    StartDate = m.StartDate,
                    DueDate = m.DueDate,
                    MaxScore = m.MaxScore,
                    IsActive = m.IsActive,
                    Attachments = m.Attachments,
                    CreatedById = m.CreatedById,
                    CreatedByFullName = m.CreatedBy != null ? m.CreatedBy.FullName : null,
                    CreatedByUserName = m.CreatedBy != null ? m.CreatedBy.UserName : null,
                    StudentId = m.StudentId,
                    StudentFullName = m.Student != null ? m.Student.FullName : null,
                    StudentUserName = m.Student != null ? m.Student.UserName : null,
                    SubmissionsCount = m.Submissions != null ? m.Submissions.Count() : 0,
                    SubmittedCount = m.Submissions != null ? m.Submissions.Count(s => !string.IsNullOrEmpty(s.SubmissionContent)) : 0,
                    GradedCount = m.Submissions != null ? m.Submissions.Count(s => s.IsGraded) : 0
                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new StudentActivityViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ClassId = m.ClassId,
                    ClassName = m.Class != null ? m.Class.Name : null,
                    ActivityType = m.ActivityType,
                    StartDate = m.StartDate,
                    DueDate = m.DueDate,
                    MaxScore = m.MaxScore,
                    IsActive = m.IsActive,
                    Attachments = m.Attachments,
                    CreatedById = m.CreatedById,
                    CreatedByFullName = m.CreatedBy != null ? m.CreatedBy.FullName : null,
                    CreatedByUserName = m.CreatedBy != null ? m.CreatedBy.UserName : null,
                    StudentId = m.StudentId,
                    StudentFullName = m.Student != null ? m.Student.FullName : null,
                    StudentUserName = m.Student != null ? m.Student.UserName : null,
                    SubmissionsCount = m.Submissions != null ? m.Submissions.Count() : 0,
                    SubmittedCount = m.Submissions != null ? m.Submissions.Count(s => !string.IsNullOrEmpty(s.SubmissionContent)) : 0,
                    GradedCount = m.Submissions != null ? m.Submissions.Count(s => s.IsGraded) : 0
                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
        }

        public async Task<Result<StudentActivityViewModel>> CreateStudentActivityAsync(StudentActivityViewModel model)
        {
            StudentActivity activity = new StudentActivity
            {
                Title = model.Title,
                Description = model.Description,
                ClassId = model.ClassId,
                ActivityType = model.ActivityType,
                StartDate = model.StartDate,
                DueDate = model.DueDate,
                MaxScore = model.MaxScore,
                IsActive = model.IsActive,
                Attachments = model.Attachments,
                IsDeleted = false,
                CreatedById = model.CreatedById,
                StudentId = model.StudentId
            };

            var result = this.Insert(activity);

            model.Id = activity.Id;

            return new Result<StudentActivityViewModel>()
            {
                Message = "Activity created successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<Result<StudentActivityViewModel>> EditStudentActivityAsync(StudentActivityViewModel model)
        {
            var activity = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
                
            if (activity == null)
            {
                return new Result<StudentActivityViewModel>()
                {
                    Message = "Activity not found",
                    Success = false
                };
            }

            activity.Title = model.Title;
            activity.Description = model.Description;
            activity.ClassId = model.ClassId;
            activity.ActivityType = model.ActivityType;
            activity.StartDate = model.StartDate;
            activity.DueDate = model.DueDate;
            activity.MaxScore = model.MaxScore;
            activity.IsActive = model.IsActive;
            activity.Attachments = model.Attachments;
            activity.CreatedById = model.CreatedById;
            activity.StudentId = model.StudentId;

            var result = this.Update(activity);

            return new Result<StudentActivityViewModel>()
            {
                Message = "Activity updated successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<StudentActivityViewModel> FindAsync(int id)
        {
            var activity = await this.GetAllAsIqueriable()
                .Include(a => a.Class)
                .Include(a => a.CreatedBy)
                .Include(a => a.Student)  // Include Student relationship
                .Include(a => a.Submissions)
                .Select(m => new StudentActivityViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ClassId = m.ClassId,
                    ClassName = m.Class != null ? m.Class.Name : null,
                    ActivityType = m.ActivityType,
                    StartDate = m.StartDate,
                    DueDate = m.DueDate,
                    MaxScore = m.MaxScore,
                    IsActive = m.IsActive,
                    Attachments = m.Attachments,
                    CreatedById = m.CreatedById,
                    CreatedByFullName = m.CreatedBy != null ? m.CreatedBy.FullName : null,
                    CreatedByUserName = m.CreatedBy != null ? m.CreatedBy.UserName : null,
                    StudentId = m.StudentId,
                    StudentFullName = m.Student != null ? m.Student.FullName : null,
                    StudentUserName = m.Student != null ? m.Student.UserName : null,
                    SubmissionsCount = m.Submissions != null ? m.Submissions.Count() : 0,
                    SubmittedCount = m.Submissions != null ? m.Submissions.Count(s => !string.IsNullOrEmpty(s.SubmissionContent)) : 0,
                    GradedCount = m.Submissions != null ? m.Submissions.Count(s => s.IsGraded) : 0
                }).SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                
            return activity;
        }

        public async Task<bool> DeleteStudentActivityAsync(int id)
        {
            try
            {
                var activity = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                if (activity != null)
                {
                    activity.IsDeleted = true;
                    this.Update(activity);
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