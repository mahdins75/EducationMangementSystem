using Implementation.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;
using DNTCaptcha.Core;
using Microsoft.Extensions.Options;
using Mstech.Wallet.ViewModel.DTO;

[Area("api/Common")]
[Route("[area]/GetDropDownData")]

public class GetDropDownDataController : SecureBaseController
{
    private readonly UserService userService;
    private readonly DNTCaptchaOptions _captchaOptions;
    private readonly IDNTCaptchaValidatorService _validatorService;

    public GetDropDownDataController(IDNTCaptchaValidatorService validatorService,
        IOptions<DNTCaptchaOptions> options,
        UserService userService

    )
    {
        _validatorService = validatorService;
        _captchaOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        this.userService = userService;
    }

    [HttpGet]
    [Route("GetDropDownUsers")]
    [AllowAnonymous]

    public async Task<JsonResult> GetDropDownUsers([FromQuery] UserViewModel model)
    {
        try
        {
            var query = userService.GetAllAsIqueriable();

            var response = new ResponseViewModel<List<SelectListItem>>();
            var result = query.Select(m => new SelectListItem() { Value = m.Id, Title = m.UserName }).ToList();
            response.Entity = result;
            response.IsSuccess = true;
            return Json(response);
        }
        catch
        {

            var response = new ResponseViewModel<List<SelectListItem>>();
            response.IsSuccess = false;
            return Json(response);
        }

    }
}
