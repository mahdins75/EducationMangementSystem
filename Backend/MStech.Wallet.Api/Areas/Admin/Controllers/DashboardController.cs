using Authorization.Custom;
using Implementation.AccessLinkeRoles;
using Implementation.DiscountCodeServiceArea;
using Implementation.ReferralCodeServiceArea;
using Implementation.RoleService;
using Implementation.TransactionServiceArea;
using Implementation.WalletClientArea;
using Implementation.WalletServiceArea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mstech.ViewModel.DTO;

[Area("api/Admin")]
[Route("[area]/DashBoard")]
[AllowAnonymous]
public class DashboardController : SecureBaseController
{
    private readonly DashboardService dashboardServices;

    public DashboardController(DashboardService dashboardServices)
    {

        this.dashboardServices = dashboardServices;


    }

    [HttpGet]
    [Route("GetDasBoardInfo")]
    public async Task<IActionResult> GetDasBoardInfo([FromQuery] RoleAccessedLinkViewModel model)
    {
        var userName = User.Identity?.Name;

        if (string.IsNullOrEmpty(userName))
        {
            return Json(new ResponseViewModel<List<RoleAccessedLinkViewModel>>() { IsSuccess = false });
        }

        var result = await dashboardServices.GetAll(userName);

        return Json(result);
    }

}
