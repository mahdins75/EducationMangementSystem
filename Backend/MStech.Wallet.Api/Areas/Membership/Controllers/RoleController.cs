using Authorization.Custom;
using Implementation.AccessLinkeRoles;
using Implementation.ConstantService;
using Implementation.FileService;
using Implementation.RoleService;
using Implementation.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;

[Area("api/Membership")]
[Route("[area]/Role")]
[SessionRequirement]
[AllowAnonymous]
public class RoleController : BaseController
{
    private readonly UserService _userService;
    private readonly FileService _fileService;
    private readonly ConstantService _constantService;
    private readonly UserManager<User> _userManager;
    public readonly RoleService _roleService;
    public readonly AccessLinkRolesService _accessLinkeRolesService;

    public RoleController(
        UserService userService,
        FileService fileService,
        UserManager<User> userManager,
        ConstantService constantService,
        RoleService roleService,
        AccessLinkRolesService accessLinkeRolesService)
    {
        _userService = userService;
        _userManager = userManager;
        _constantService = constantService;
        _roleService = roleService;
        _accessLinkeRolesService = accessLinkeRolesService;
        _fileService = fileService;
    }

    [HttpGet]
    [Route("GetAll")]
    [SessionRequirement]
    public async Task<IActionResult> GetAll([FromQuery] RoleViewModel model)
    {
        var result = await _roleService.GetAllRoles(model);

        return Json(new ResponseViewModel<List<RoleViewModel>>() { IsSuccess = true, Entity = result.Entity, Message = result.Message, QueryCount = result.QueryCount });
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
