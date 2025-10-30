using Authorization.Custom;
using Implementation.AccessLinkeRoles;
using Implementation.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mstech.ViewModel.DTO;

[Area("api/Admin")]
[Route("[area]/Role")]
[SessionRequirement]
[AllowAnonymous]
public class AccessLinkRole : BaseController
{
    private readonly AccessLinkRolesService accessLinkRolesService;
    private readonly RoleService _roleService;
    public AccessLinkRole(
        AccessLinkRolesService accessLinkRolesService, RoleService roleService)
    {
        this.accessLinkRolesService = accessLinkRolesService;
        this._roleService = roleService;
    }

    [HttpGet]
    [Route("GetAll")]
    [SessionRequirement]
    public async Task<IActionResult> GetAll([FromQuery] RoleAccessedLinkViewModel model)
    {
        var result = await accessLinkRolesService.GetAllAsync(model);

        return Json(new ResponseViewModel<List<RoleAccessedLinkViewModel>>() { IsSuccess = true, Entity = result.Entity, Message = result.Message, QueryCount = result.QueryCount });
    }
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] RoleViewModel model)
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

            string validationMessages = string.Join(" | ", errorProps.Select(e => $"{e.Property}: {e.Errors}"));

            return Json(new ResponseViewModel<RoleViewModel>
            {
                IsSuccess = false,
                Entity = model,
                Message = validationMessages
            });
        }


        var result = await _roleService.CreateAsync(model);

        if (!result.IsSuccess)
            return Json(new ResponseViewModel<RoleViewModel> { IsSuccess = false, Entity = model, Message = result.Message });

        return Json(new ResponseViewModel<RoleViewModel> { IsSuccess = true });

    }

    [HttpPost]
    [Route("Edit")]
    public async Task<IActionResult> Edit([FromBody] RoleViewModel model)
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

            string validationMessages = string.Join(" | ", errorProps.Select(e => $"{e.Property}: {e.Errors}"));

            return Json(new ResponseViewModel<RoleViewModel>
            {
                IsSuccess = false,
                Entity = model,
                Message = validationMessages
            });
        }

        var result = await _roleService.EditAsync(model);
        if (!result.IsSuccess)
            return Json(new ResponseViewModel<RoleViewModel> { IsSuccess = false, Entity = model, Message = result.Message });

        return Json(new ResponseViewModel<RoleViewModel> { IsSuccess = true });
    }

    [HttpPost]
    [Route("Delete")]
    [AllowAnonymous]
    [SessionRequirement]
    public async Task<IActionResult> Delete([FromBody] RoleViewModel model)
    {
        var result = await _roleService.DeleteAsync(model.Id);

        return Json(new ResponseViewModel<RoleViewModel> { IsSuccess = true });
    }


}
