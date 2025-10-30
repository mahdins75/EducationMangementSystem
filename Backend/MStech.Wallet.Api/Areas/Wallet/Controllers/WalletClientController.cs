using Microsoft.AspNetCore.Mvc;
using Implementation.WalletClientArea;
using Mstech.ViewModel.DTO;
using Implementation.WalletServiceArea;

[Area("api/Wallet")]
[Route("[area]/WalletClient")]
public class WalletClientController : SecureBaseController
{
    private readonly WalletClientService walletClientService;
    private readonly WalletService walletService;
    public WalletClientController(WalletClientService walletClientService, WalletService walletService)
    {
        this.walletClientService = walletClientService;
        this.walletService = walletService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<JsonResult> GetAll([FromQuery] WalletClientViewModel model)
    {
        var result = await walletClientService.GetAll(model);

        return Json(new ResponseViewModel<List<WalletClientViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }

    [HttpGet]
    [Route("GetAllOfMyUsers")]
    public async Task<JsonResult> GetAllOfMyUsers([FromQuery] WalletClientViewModel model)
    {
        var result = await walletClientService.GetAll(model);

        return Json(new ResponseViewModel<List<WalletClientViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }

    [HttpPost]
    [Route("Create")]
    public async Task<JsonResult> Create([FromBody] WalletClientViewModel model)
    {
        var result = await walletClientService.CreateWalletClientAsync(model);
        if (result.Success)
        {
            await walletService.CreateWalletAsync(new WalletViewModel() { UserId = model.OwnerId, WalletType = MStech.Accounting.DataBase.Enums.WalletType.ClientOwner, ClientId = result.Entity.Id });

        }

        return Json(new ResponseViewModel<WalletClientViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success });
    }

    [HttpPost]
    [Route("Update")]

    public async Task<JsonResult> Update([FromBody] WalletClientViewModel model)
    {
        var result = await walletClientService.EditWalletClientAsync(model);

        return Json(new ResponseViewModel<WalletClientViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success });
    }

    [HttpPost]
    [Route("Delete")]
    public async Task<JsonResult> Delete([FromBody] WalletClientViewModel model)
    {
        var result = await walletClientService.DeleteWalletClientAsync(model.Id);

        return Json(new ResponseViewModel<bool>() { Entity = result, Message = "DeleteOperationIsDone", IsSuccess = result });
    }
}
