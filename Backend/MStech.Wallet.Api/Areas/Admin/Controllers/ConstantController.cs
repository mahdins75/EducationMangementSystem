using Implementation.ConstantService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mstech.ADV.Keys;
using Mstech.ViewModel.DTO;

[Area("Admin")]
[Route("[area]/Constant")]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class ConstantController : BaseController
{
    private readonly ConstantService _constantService;


    public ConstantController(ConstantService constantService)
    {
        _constantService = constantService;
    }

    [HttpGet]
    [Route("Index/{entityName}")]
    public async Task<IActionResult> Index(string entityName)
    {
        if (entityName != ConstantEntityNames.__Tag && entityName != ConstantEntityNames.__Source
            && entityName != ConstantEntityNames.__UserPosition && entityName != ConstantEntityNames.__Department)
        {
            return View("NotFound");
        }

        var model = new ConstantFilterViewModel();
        model.EntityName = entityName;
        return View(model);
    }

    [HttpPost]
    [Route("ConstantDataTable")]
    public async Task<IActionResult> ConstantDataTable(ConstantDataTableViewModel model)
    {
        var result = await _constantService.LoadDataTable(model);

        return Json(new
        {
            draw = model.Draw,
            recordsTotal = result.RecordsTotal,
            recordsFiltered = result.RecordsFiltered,
            data = result.Data
        });
    }

    [HttpGet]
    [Route("GetConstant")]
    public async Task<IActionResult> GetConstant(int id)
    {
        var constant = await _constantService.GetById(id);

        return Json(new { success = true, data = constant });
    }


    [HttpPost]
    [Route("Manage")]
    public async Task<IActionResult> Manage(ManageConstantViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errorProps = new List<AjaxModalValidation>();
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                if (modelStateEntry != null && modelStateEntry.ValidationState == ModelValidationState.Invalid)
                {
                    errorProps.Add(new AjaxModalValidation()
                    {
                        Property = key,
                        Errors = modelStateEntry.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    });
                }
            }

            return Json(new { success = false, isValid = false, errors = errorProps });
        }

        string errorMessage = "";
        int result = 0;
        if (model.Id.HasValue)
        {
            result = await _constantService.EditAsync(model);
        }
        else
        {
            result = await _constantService.CreateAsync(model);
        }

        if (result == 0)
            errorMessage = "عملیات با خطا مواجه شد.";

        if (result == -1)
            errorMessage = "عنوان وارد شده تکراری است.";

        if (errorMessage != "")
            return Json(new { success = false, isValid = true, message = errorMessage });

        return Json(new { success = true });

    }

    [HttpPost]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _constantService.DeleteAsync(id);
        return Json(new { success = result });
    }

}
