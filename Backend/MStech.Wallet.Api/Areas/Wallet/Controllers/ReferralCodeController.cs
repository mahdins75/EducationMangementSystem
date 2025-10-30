using Microsoft.AspNetCore.Mvc;
using Implementation.WalletClientArea;
using Mstech.ViewModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Implementation.WalletServiceArea;
using MStech.Wallet.DataBase.Etity.Wallet;
using Implementation.ReferralCodeServiceArea;

[Area("api/Wallet")]
[Route("[area]/ReferralCode")]
[AllowAnonymous]
public class ReferralCodeController : SecureBaseController
{
    private readonly ReferralCodeService referralCodeService;

    public ReferralCodeController(ReferralCodeService referralCodeService)
    {
        this.referralCodeService = referralCodeService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<JsonResult> GetAll([FromQuery] ReferralCodeViewModel model)
    {
        var result = await referralCodeService.GetAll(model);

        return Json(new ResponseViewModel<List<ReferralCodeViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.IsSuccess, QueryCount = result.QueryCount });
    }

    [HttpPost]
    [Route("Create")]
    [AllowAnonymous]
    public async Task<JsonResult> Create([FromBody] ReferralCodeViewModel model)
    {
        var result = await referralCodeService.CreateReferralCodeAsync(model);

        return Json(new ResponseViewModel<ReferralCodeViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.IsSuccess });
    }

    [HttpPost]
    [Route("Update")]
    [AllowAnonymous]

    public async Task<JsonResult> Update([FromBody] ReferralCodeViewModel model)
    {
        var result = await referralCodeService.EditReferralCodeAsync(model);

        return Json(new ResponseViewModel<ReferralCodeViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.IsSuccess });
    }

    [HttpPost]
    [Route("Delete")]
    [AllowAnonymous]
    public async Task<JsonResult> Delete([FromBody] ReferralCodeViewModel model)
    {
        var result = await referralCodeService.DeleteReferralCodeAsync(model.Id);

        return Json(new ResponseViewModel<bool>() { Entity = result, Message = "DeleteOperationIsDone", IsSuccess = result });
    }
}
