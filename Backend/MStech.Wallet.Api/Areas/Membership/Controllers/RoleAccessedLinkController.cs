using Authorization.Custom;
using Implementation.AccessedLinkService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;

[Area("api/Membership")]
[Route("[area]/RoleAccessedLink")]
[AllowAnonymous]
[SessionRequirement]

public class RoleAccessedLinkController : BaseController
{

    private readonly RoleAccessedLinkService roleAccessedLinkService;

    private readonly AccessedLinkService accessedLinkService;
    public RoleAccessedLinkController(RoleAccessedLinkService roleAccessedLinkService, AccessedLinkService accessedLinkService)
    {
        this.roleAccessedLinkService = roleAccessedLinkService;

        this.accessedLinkService = accessedLinkService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<JsonResult> GetAll([FromQuery] RoleAccessedLinkViewModel model)
    {
        var result = await roleAccessedLinkService.GetAll(new RoleAccessedLinkViewModel() { RoleId = model.RoleId });

        return Json(result);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleAccessLinkViewmodel model)
    {
        var newItems = new List<RoleAccessedLinkViewModel>();

        foreach (var item in model.Links)
        {
            newItems.Add(new RoleAccessedLinkViewModel() { RoleId = model.RoleId, AccessedLinkId = item });
        }
        var deleteItem = await roleAccessedLinkService.GetAll(new RoleAccessedLinkViewModel() { RoleId = model.RoleId });

        foreach (var item in deleteItem.Entity)
        {
            var deleteResult = await roleAccessedLinkService.HardDeletemodel(new RoleAccessedLinkViewModel() { Id = item.Id });
        }

        var result = await roleAccessedLinkService.InsertRangeModel(newItems);

        return Ok(result);
    }


}
