using Implementation.AccessedLinkService;
using Implementation.AccessLinkeRoles;
using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;

[Area("api/Admin")]
[Route("[area]/AccessedLink")]
public class AccessedLinkController : BaseController
{
    private readonly AccessedLinkService accessedLinkService;

    public AccessedLinkController(AccessedLinkService accessedLinkService, AccessLinkRolesService accessLinkeRolesService)
    {
        this.accessedLinkService = accessedLinkService;
    }

    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await accessedLinkService.GetAccessedLinkList();

        return Json(new ResponseViewModel<List<AccessedLinkViewModel>>() { IsSuccess = true, Entity = result });
    }

    [HttpPost]
    public async Task<IActionResult> UserDataTable(AccessedLinkDataTableViewModel model)
    {
        var result = await accessedLinkService.LoadDataTable(model);

        return Json(new
        {
            draw = model.Draw,
            recordsTotal = result.RecordsTotal,
            recordsFiltered = result.RecordsFiltered,
            data = result.Data
        });
    }
}