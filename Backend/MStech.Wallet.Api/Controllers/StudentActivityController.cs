using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;
using Implementation.StudentActivityArea;
using ViewModel.Infrastructure;

namespace Mstech.ADV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentActivityController : ControllerBase
    {
        private readonly StudentActivityService _studentActivityService;

        public StudentActivityController(StudentActivityService studentActivityService)
        {
            _studentActivityService = studentActivityService;
        }

        [HttpPost("getall")]
        public async Task<IActionResult> GetAll([FromBody] StudentActivityViewModel model)
        {
            var result = await _studentActivityService.GetAll(model);
            return Ok(new ResponseViewModel<List<StudentActivityViewModel>>
            {
                IsSuccess = result.Success,
                Message = result.Message,
                Entity = result.Entity,
                QueryCount = result.Count
            });
        }

        [HttpGet("bystudent/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            // Create a model to search by student ID
            var model = new StudentActivityViewModel { StudentId = studentId.ToString() };
            var result = await _studentActivityService.GetAll(model);
            return Ok(new ResponseViewModel<List<StudentActivityViewModel>>
            {
                IsSuccess = result.Success,
                Message = result.Message,
                Entity = result.Entity,
                QueryCount = result.Count
            });
        }

        [HttpGet("bytype/{activityType}")]
        public async Task<IActionResult> GetByActivityType(string activityType)
        {
            // Create a model to search by activity type
            var model = new StudentActivityViewModel { ActivityTypeFilter = activityType };
            var result = await _studentActivityService.GetAll(model);
            return Ok(new ResponseViewModel<List<StudentActivityViewModel>>
            {
                IsSuccess = result.Success,
                Message = result.Message,
                Entity = result.Entity,
                QueryCount = result.Count
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentActivityService.FindAsync(id);
            return Ok(new ResponseViewModel<StudentActivityViewModel>
            {
                IsSuccess = true,
                Entity = result,
                QueryCount = result != null ? 1 : 0
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateStudentActivityViewModel model)
        {
            var activityModel = new StudentActivityViewModel
            {
                Title = model.ActivityTitle,
                Description = model.Description,
                ClassId = model.ClassId,
                ActivityType = model.ActivityType,
                StartDate = model.ActivityDateTime,
                DueDate = model.ActivityDateTime,
                MaxScore = (int)model.Score,
                IsActive = true,
                CreatedById = "TODO", // This should be obtained from current user
                StudentId = model.StudentId,
                StudentFullName = model.StudentFullName
            };

            var result = await _studentActivityService.CreateStudentActivityAsync(activityModel);
            return Ok(new ResponseViewModel<List<StudentActivityViewModel>>
            {
                IsSuccess = result.Success,
                Message = result.Message,
                Entity = new List<StudentActivityViewModel> { result.Entity },
                QueryCount = 1
            });
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateStudentActivityViewModel model)
        {
            var activityModel = new StudentActivityViewModel
            {
                Id = model.Id,
                Title = model.ActivityTitle,
                Description = model.Description,
                ClassId = model.ClassId,
                ActivityType = model.ActivityType,
                StartDate = model.ActivityDateTime,
                DueDate = model.ActivityDateTime,
                MaxScore = (int)model.Score,
                IsActive = true,
                CreatedById = "TODO", // This should be obtained from current user
                StudentId = model.StudentId,
                StudentFullName = model.StudentFullName
            };

            var result = await _studentActivityService.EditStudentActivityAsync(activityModel);
            return Ok(new ResponseViewModel<StudentActivityViewModel>
            {
                IsSuccess = result.Success,
                Message = result.Message,
                Entity = result.Entity,
                QueryCount = 1
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentActivityService.DeleteStudentActivityAsync(id);
            return Ok(new ResponseViewModel<bool>
            {
                IsSuccess = result,
                Entity = result,
                QueryCount = result ? 1 : 0
            });
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchActivities(string searchTerm)
        {
            var model = new StudentActivityViewModel { SearchTitle = searchTerm };
            var result = await _studentActivityService.GetAll(model);
            return Ok(new ResponseViewModel<List<StudentActivityViewModel>>
            {
                IsSuccess   = result.Success,
                Message = result.Message,
                Entity = result.Entity,
                QueryCount= result.Count
            });
        }
    }

    
}