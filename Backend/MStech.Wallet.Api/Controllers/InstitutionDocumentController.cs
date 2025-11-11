using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;
using Implementation.InstitutionArea;
using ViewModel.Infrastructure;

namespace Mstech.ADV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitutionDocumentController : ControllerBase
    {
        private readonly InstitutionDocumentService _institutionDocumentService;

        public InstitutionDocumentController(InstitutionDocumentService institutionDocumentService)
        {
            _institutionDocumentService = institutionDocumentService;
        }

        [HttpPost("getall")]
        public async Task<IActionResult> GetAll([FromBody] InstitutionDocumentViewModel model)
        {
            var result = await _institutionDocumentService.GetAll(model);
            return Ok(new ResponseViewModel<List<InstitutionDocumentViewModel>>
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
            var result = await _institutionDocumentService.FindAsync(id);
            return Ok(new ResponseViewModel<InstitutionDocumentViewModel>
            {
                IsSuccess = true,
                Entity = result,
                QueryCount = result != null ? 1 : 0
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] InstitutionDocumentViewModel model)
        {
            var result = await _institutionDocumentService.CreateInstitutionDocumentAsync(model);
            return Ok(new ResponseViewModel<List<InstitutionDocumentViewModel>>
            {
                IsSuccess = result.Success,
                Message = result.Message,
                Entity = new List<InstitutionDocumentViewModel> { result.Entity },
                QueryCount = 1
            });
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InstitutionDocumentViewModel model)
        {
            var result = await _institutionDocumentService.EditInstitutionDocumentAsync(model);
            return Ok(new ResponseViewModel<InstitutionDocumentViewModel>
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
            var result = await _institutionDocumentService.DeleteInstitutionDocumentAsync(id);
            return Ok(new ResponseViewModel<bool>
            {
                IsSuccess = result,
                Entity = result,
                QueryCount = result ? 1 : 0
            });
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchInstitutionDocuments(string searchTerm)
        {
            var model = new InstitutionDocumentViewModel { SearchTitle = searchTerm };
            var result = await _institutionDocumentService.GetAll(model);
            return Ok(new ResponseViewModel<List<InstitutionDocumentViewModel>>
            {
                IsSuccess = result.Success,
                Message = result.Message,
                Entity = result.Entity,
                QueryCount = result.Count
            });
        }
    }
}