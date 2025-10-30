using Microsoft.AspNetCore.Mvc;
using Implementation.WalletClientArea;
using Mstech.ViewModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Implementation.WalletServiceArea;
using Implementation.UserService;

[Area("api/Wallet")]
[Route("[area]/Wallet")]
public class WalletController : SecureBaseController
{
    private readonly WalletService walletService;
    private readonly UserService userService;
    private readonly WalletClientService walletClientService;


    public WalletController(WalletService walletService, UserService userService, WalletClientService walletClientService)
    {
        this.walletService = walletService;
        this.userService = userService;
        this.walletClientService = walletClientService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<JsonResult> GetAll([FromQuery] WalletViewModel model)
    {
        var result = await walletService.GetAll(model);

        return Json(new ResponseViewModel<List<WalletViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }

    [HttpGet]
    [Route("GetAllForClientOwner")]
    public async Task<JsonResult> GetAllForClientOwner([FromQuery] WalletViewModel model)
    {
        model.ClientOwnerUserName = User.Identity.Name;
        if (model.ClientId <= 0)
        {
            return Json(new ResponseViewModel<List<WalletViewModel>>() { Entity = null, Message = "failure", IsSuccess = false, QueryCount = 0 });
        }

        var result = await walletService.GetAll(model);

        return Json(new ResponseViewModel<List<WalletViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }

    [HttpGet]
    [Route("GetAllForCurrentUser")]
    public async Task<JsonResult> GetAllForCurrentUser([FromQuery] WalletViewModel model)
    {
        var userName = User.Identity.Name;

        if (string.IsNullOrEmpty(userName))
        {
            return Json(new ResponseViewModel<List<WalletViewModel>>() { Message = "User Not Fount", IsSuccess = false });
        }

        var user = await userService.FindByUserNameAsync(userName);

        if (user == null)
        {
            return Json(new ResponseViewModel<List<WalletViewModel>>() { Message = "User Not Fount", IsSuccess = false });
        }

        model.UserId = user.Id;

        var result = await walletService.GetAll(model);

        return Json(new ResponseViewModel<List<WalletViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }
    [HttpPost]
    [Route("Create")]
    [AllowAnonymous]
    public async Task<JsonResult> Create([FromBody] WalletViewModel model)
    {
        var result = await walletService.CreateWalletAsync(model);

        return Json(new ResponseViewModel<WalletViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success });
    }

    [HttpPost]
    [Route("Update")]
    [AllowAnonymous]

    public async Task<JsonResult> Update([FromBody] WalletViewModel model)
    {
        var result = await walletService.EditWalletAsync(model);

        return Json(new ResponseViewModel<WalletViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success });
    }

    [HttpPost]
    [Route("Delete")]
    [AllowAnonymous]
    public async Task<JsonResult> Delete([FromBody] WalletViewModel model)
    {
        var result = await walletService.DeleteWalletAsync(model.Id);

        return Json(new ResponseViewModel<bool>() { Entity = result, Message = "DeleteOperationIsDone", IsSuccess = result });
    }


    //////client side
    /// 


    [HttpGet]
    [AllowAnonymous]
    [Route("GetAllForClient")]
    public async Task<JsonResult> GetAllForClient([FromQuery] WalletViewModel model)
    {
        if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        if (Request.Headers.TryGetValue("username", out var userName))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }
        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName }).Result.Entity.Any();

        if (!clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        model.Client = new WalletClientViewModel() { ClientIdForApi = clientId };
        var result = await walletService.GetAll(model);

        return Json(new ResponseViewModel<List<WalletViewModel>>() { Entity = result.Entity != null ? result.Entity : new List<WalletViewModel>(), Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("GetAllForCurrentUserForClient")]
    public async Task<JsonResult> GetAllForCurrentUserForClient([FromQuery] WalletViewModel model)
    {
        if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        if (Request.Headers.TryGetValue("username", out var userName))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName }).Result.Entity.Any();

        if (clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var currentUserName = User.Identity.Name;

        if (string.IsNullOrEmpty(userName))
        {
            return Json(new ResponseViewModel<List<WalletViewModel>>() { Message = "User Not Fount", IsSuccess = false });
        }

        var user = await userService.FindByUserNameAsync(userName);

        if (user == null)
        {
            return Json(new ResponseViewModel<List<WalletViewModel>>() { Message = "User Not Fount", IsSuccess = false });
        }

        model.UserId = user.Id;

        var result = await walletService.GetAll(model);

        return Json(new ResponseViewModel<List<WalletViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }

    [HttpPost]
    [Route("CreateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> CreateForClient([FromBody] WalletViewModel model)
    {
        if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        if (Request.Headers.TryGetValue("username", out var userName))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName }).Result.Entity.Any();

        if (clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        if (clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var result = await walletService.CreateWalletAsync(model);

        return Json(new ResponseViewModel<WalletViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success });
    }

    [HttpPost]
    [Route("UpdateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> UpdateForClient([FromBody] WalletViewModel model)
    {
        if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        if (Request.Headers.TryGetValue("username", out var userName))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName }).Result.Entity.Any();

        if (clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        if (clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var result = await walletService.EditWalletAsync(model);

        return Json(new ResponseViewModel<WalletViewModel>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success });
    }

    [HttpPost]
    [Route("DeleteForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> DeleteForClient([FromBody] WalletViewModel model)
    {
        if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }
        if (Request.Headers.TryGetValue("username", out var userName))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName }).Result.Entity.Any();

        if (clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var result = await walletService.DeleteWalletAsync(model.Id);

        return Json(new ResponseViewModel<bool>() { Entity = result, Message = "DeleteOperationIsDone", IsSuccess = result });
    }

}
