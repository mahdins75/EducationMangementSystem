using System.Security.Claims;
using Implementation.AccessedLinkService;
using Implementation.UserService;
using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;
using DNTCaptcha.Core;
using Microsoft.Extensions.Options;
using Mstech.Wallet.ViewModel.DTO;
using System.Data;

[Area("api/Common")]
[Route("[area]/Common")]
public class CommonController : SecureBaseController
{
    private readonly UserService userService;

    private readonly DNTCaptchaOptions _captchaOptions;

    private readonly IDNTCaptchaValidatorService _validatorService;

    public readonly RoleAccessedLinkService roleAccessedLinkService;

    public CommonController(IDNTCaptchaValidatorService validatorService,
        IOptions<DNTCaptchaOptions> options,
        UserService userService,
        RoleAccessedLinkService roleAccessedLinkService)
    {
        _validatorService = validatorService;

        _captchaOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));

        this.userService = userService;

        this.roleAccessedLinkService = roleAccessedLinkService;

    }

    [HttpGet]
    [Route("GetNav")]
    public async Task<JsonResult> GetNav()
    {
        var userName = User.Identity.Name;

        var response = new ResponseViewModel<List<NavItemViewModel>>();

        if (string.IsNullOrEmpty(userName))
        {
            return Json(response);
        }

        var role = User.Claims.Where(c => c.Type == ClaimTypes.Role)?.Select(m => m.Value).ToList();

        var navItems = await this.roleAccessedLinkService.GetMenuItemsInHTML(userName, role);

        response.IsSuccess = true;

        response.Entity = navItems;

        return Json(response);
    }

    [HttpGet]
    [Route("CheckAuthentication")]
    public async Task<JsonResult> CheckAuthentication()
    {
        return Json(new ResponseViewModel<UserViewModel>() { IsSuccess = true });
    }
}
