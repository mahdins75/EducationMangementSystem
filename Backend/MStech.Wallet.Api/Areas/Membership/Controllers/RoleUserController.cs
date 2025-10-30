using Authorization.Custom;
using Implementation.AccessLinkeRoles;
using Implementation.ConstantService;
using Implementation.FileService;
using Implementation.RoleUserService;
using Implementation.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;

[Area("api/Membership")]
[Route("[area]/RoleUser")]
[SessionRequirement]
[AllowAnonymous]
public class RoleUserController : BaseController
{
    private readonly UserService _userService;
    private readonly FileService _fileService;
    private readonly ConstantService _constantService;
    private readonly UserManager<User> _userManager;
    public readonly RoleUserService _roleUserService;
    public readonly AccessLinkRolesService _accessLinkeRolesService;

    public RoleUserController(
        UserService userService,
        FileService fileService,
        UserManager<User> userManager,
        ConstantService constantService,
        RoleUserService roleUserService,
        AccessLinkRolesService accessLinkeRolesService)
    {
        _userService = userService;
        _userManager = userManager;
        _constantService = constantService;
        _roleUserService = roleUserService;
        _accessLinkeRolesService = accessLinkeRolesService;
        _fileService = fileService;
    }

    [HttpGet]
    [Route("GetAll")]
    [SessionRequirement]
    public async Task<IActionResult> GetAll([FromQuery] RoleUserViewModel model)
    {
        var result = await _roleUserService.GetAllUserRoleEntity(model.UserId);
        return Json(new ResponseViewModel<List<RoleUserViewModel>>() { IsSuccess = true, Entity = result.Entity, Message = result.Message, QueryCount = result.QueryCount });
    }
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] RoleUserViewModel model)
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

            return Json(new ResponseViewModel<RoleUserViewModel>
            {
                IsSuccess = false,
                Entity = model,
                Message = validationMessages
            });
        }


        var result = await _roleUserService.Insertmodel(model);

        if (!result.IsSuccess)
            return Json(new ResponseViewModel<RoleUserViewModel> { IsSuccess = false, Entity = model, Message = result.Message });

        return Json(new ResponseViewModel<RoleUserViewModel> { IsSuccess = true });

    }

    [HttpPost]
    [Route("Edit")]
    public async Task<IActionResult> Edit([FromBody] RoleUserViewModel model)
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

            return Json(new ResponseViewModel<RoleUserViewModel>
            {
                IsSuccess = false,
                Entity = model,
                Message = validationMessages
            });
        }

        var result = await _roleUserService.Updatemodel(model);
        if (!result.IsSuccess)
            return Json(new ResponseViewModel<RoleUserViewModel> { IsSuccess = false, Entity = model, Message = result.Message });

        return Json(new ResponseViewModel<RoleUserViewModel> { IsSuccess = true });
    }

    [HttpPost]
    [Route("Delete")]
    [AllowAnonymous]
    [SessionRequirement]
    public async Task<IActionResult> Delete([FromBody] RoleUserViewModel model)
    {
        var result = await _roleUserService.Deletemodel(model.Id);

        return Json(new ResponseViewModel<RoleUserViewModel> { IsSuccess = true });
    }


}
