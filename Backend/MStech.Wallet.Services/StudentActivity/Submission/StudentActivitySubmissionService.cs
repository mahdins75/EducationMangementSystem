using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using ViewModel.Infrastructure;
using MStech.Wallet.DataBase.Etity.StudentActivity.Submission;
using Mstech.Entity.Etity;
using Mstech.Accounting.Data;

namespace Implementation.StudentActivityArea.Submission
{
    public class StudentActivitySubmissionService : BaseService<StudentActivitySubmission>
    {
        private readonly ApplicationDbContext db;
        public StudentActivitySubmissionService(IUnitOfWork _unitOfWork, IRepository<StudentActivitySubmission> _repositorys, ApplicationDbContext db) : base(_unitOfWork)
        {
            this.db = db;
        }

        public async Task<Result<List<StudentActivitySubmissionViewModel>>> GetAll(StudentActivitySubmissionViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Include(m => m.Activity)
                .Include(m => m.Student)
                .Where(x => !x.IsDeleted);

            var result = new Result<List<StudentActivitySubmissionViewModel>>();
            
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.SearchStudentName))
            {
                query = query.Where(m => m.Student.FullName.Contains(model.SearchStudentName) || 
                                        m.Student.UserName.Contains(model.SearchStudentName));
            }
            
            if (model.ActivityIdFilter.HasValue && model.ActivityIdFilter > 0)
            {
                query = query.Where(m => m.ActivityId == model.ActivityIdFilter);
            }
            
            if (model.IsGradedFilter.HasValue)
            {
                query = query.Where(m => m.IsGraded == model.IsGradedFilter.Value);
            }
            
            if (!string.IsNullOrEmpty(model.StudentId))
            {
                query = query.Where(m => m.StudentId == model.StudentId);
            }

            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new StudentActivitySubmissionViewModel()
                {
                    Id = m.Id,
                    ActivityId = m.ActivityId,
                    StudentId = m.StudentId,
                    StudentFullName = m.Student != null ? m.Student.FullName : null,
                    StudentUserName = m.Student != null ? m.Student.UserName : null,
                    SubmissionContent = m.SubmissionContent,
                    SubmissionDate = m.SubmissionDate,
                    Score = m.Score,
                    Comments = m.Comments,
                    IsGraded = m.IsGraded
                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new StudentActivitySubmissionViewModel()
                {
                    Id = m.Id,
                    ActivityId = m.ActivityId,
                    StudentId = m.StudentId,
                    StudentFullName = m.Student != null ? m.Student.FullName : null,
                    StudentUserName = m.Student != null ? m.Student.UserName : null,
                    SubmissionContent = m.SubmissionContent,
                    SubmissionDate = m.SubmissionDate,
                    Score = m.Score,
                    Comments = m.Comments,
                    IsGraded = m.IsGraded
                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
        }

        public async Task<Result<StudentActivitySubmissionViewModel>> CreateStudentActivitySubmissionAsync(StudentActivitySubmissionViewModel model)
        {
            StudentActivitySubmission submission = new StudentActivitySubmission
            {
                ActivityId = model.ActivityId,
                StudentId = model.StudentId,
                SubmissionContent = model.SubmissionContent,
                SubmissionDate = DateTime.Now,
                Score = model.Score,
                Comments = model.Comments,
                IsGraded = model.IsGraded,
                IsDeleted = false
            };

            var result = this.Insert(submission);

            model.Id = submission.Id;

            return new Result<StudentActivitySubmissionViewModel>()
            {
                Message = "Submission created successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<Result<StudentActivitySubmissionViewModel>> EditStudentActivitySubmissionAsync(StudentActivitySubmissionViewModel model)
        {
            var submission = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
                
            if (submission == null)
            {
                return new Result<StudentActivitySubmissionViewModel>()
                {
                    Message = "Submission not found",
                    Success = false
                };
            }

            submission.ActivityId = model.ActivityId;
            submission.StudentId = model.StudentId;
            submission.SubmissionContent = model.SubmissionContent;
            submission.SubmissionDate = model.SubmissionDate;
            submission.Score = model.Score;
            submission.Comments = model.Comments;
            submission.IsGraded = model.IsGraded;

            var result = this.Update(submission);

            return new Result<StudentActivitySubmissionViewModel>()
            {
                Message = "Submission updated successfully",
                Success = true,
                Entity = model
            };
        }

        public async Task<StudentActivitySubmissionViewModel> FindAsync(int id)
        {
            var submission = await this.GetAllAsIqueriable()
                .Include(s => s.Activity)
                .Include(s => s.Student)
                .Select(m => new StudentActivitySubmissionViewModel()
                {
                    Id = m.Id,
                    ActivityId = m.ActivityId,
                    StudentId = m.StudentId,
                    StudentFullName = m.Student != null ? m.Student.FullName : null,
                    StudentUserName = m.Student != null ? m.Student.UserName : null,
                    SubmissionContent = m.SubmissionContent,
                    SubmissionDate = m.SubmissionDate,
                    Score = m.Score,
                    Comments = m.Comments,
                    IsGraded = m.IsGraded
                }).SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                
            return submission;
        }

        public async Task<bool> DeleteStudentActivitySubmissionAsync(int id)
        {
            try
            {
                var submission = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                if (submission != null)
                {
                    submission.IsDeleted = true;
                    this.Update(submission);
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