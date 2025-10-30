using Microsoft.AspNetCore.Mvc;
using Implementation.WalletClientArea;
using Mstech.ViewModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Implementation.DiscountCodeServiceArea;
using Implementation.UserService;
using Microsoft.EntityFrameworkCore;

[Area("api/Wallet")]
[Route("[area]/DiscountCodeBank")]
public class DiscountCodeBankController : SecureBaseController
{
    private readonly DiscountCodeBankService DiscountCodeBankService;
    private readonly WalletClientService walletClientService;
    private readonly UserService userService;

    public DiscountCodeBankController(DiscountCodeBankService DiscountCodeBankService, WalletClientService walletClientService, UserService userService)
    {
        this.DiscountCodeBankService = DiscountCodeBankService;
        this.walletClientService = walletClientService;
        this.userService = userService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<JsonResult> GetAll([FromQuery] DiscountCodeBankViewModel model)
    {
        var result = await DiscountCodeBankService.GetAll(model);

        return Json(result);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<JsonResult> Create([FromBody] DiscountCodeBankViewModel model)
    {
        var result = await DiscountCodeBankService.CreateDiscountCodeAsync(model);

        return Json(result);

    }

    [HttpPost]
    [Route("Update")]

    public async Task<JsonResult> Update([FromBody] DiscountCodeBankViewModel model)
    {
        var result = await DiscountCodeBankService.EditDiscountCodeAsync(model);

        return Json(result);

    }

    [HttpPost]
    [Route("Delete")]
    public async Task<JsonResult> Delete([FromBody] DiscountCodeBankViewModel model)
    {
        var result = await DiscountCodeBankService.DeleteDiscountCodeAsync(model.Id);

        return Json(result);
    }


    //////client side
    /// 


    [HttpGet]
    [Route("GetAllForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> GetAllForClient([FromQuery] DiscountCodeBankViewModel model)
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

        var user = await userService.GetAll(new UserViewModel() { UserName = userName, Id = "" });

        if (user == null || !user.Success || !user.Entity.Any())
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کاربر غیر مجاز",
                IsSuccess = false
            });
        }


        var owner = await userService.GetAllAsIqueriable().Where(m => m.UserName == model.OwnerUserName).FirstOrDefaultAsync();
        if (owner == null && !string.IsNullOrEmpty(model.OwnerUserName))
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کاربر غیر مجاز",
                IsSuccess = false
            });
        }


        model.OwnerId = owner != null ? owner.Id.ToString() : "";
        model.ClientIdForApi = clientId;
        var result = await DiscountCodeBankService.GetAll(model);

        return Json(result);
    }

    [HttpGet]
    [Route("GetCountForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> GetCountForClient([FromQuery] DiscountCodeBankViewModel model)
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



        var user = await userService.GetAll(new UserViewModel() { UserName = model.OwnerUserName, Id = "" });
        if (user == null || !user.Success || !user.Entity.Any())
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کاربر غیر مجاز",
                IsSuccess = false
            });
        }

        var result = DiscountCodeBankService.GetAll(new DiscountCodeBankViewModel() { ClientIdForApi = clientId, OwnerId = user.Entity.FirstOrDefault().Id }).Result.Entity.Count();

        var resposne = new ResponseViewModel<int>();

        resposne.Entity = result;

        resposne.IsSuccess = true;

        return Json(resposne);
    }

    [HttpPost]
    [Route("CreateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> CreateForClient([FromBody] DiscountCodeBankViewModel model)
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

        var user = await userService.GetAll(new UserViewModel() { UserName = userName, Id = "" });

        model.OwnerId = user.Entity.FirstOrDefault().Id.ToString();
        model.ClientIdForApi = clientId;

        var owner = await userService.GetAllAsIqueriable().Where(m => m.UserName == model.OwnerUserName).FirstOrDefaultAsync();
        if (owner == null)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کاربر غیر مجاز",
                IsSuccess = false
            });
        }

        model.OwnerId = owner.Id.ToString();
        var result = await DiscountCodeBankService.CreateDiscountCodeAsync(model);
        return Json(result);

    }

    [HttpPost]
    [Route("UpdateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> UpdateForClient([FromBody] DiscountCodeBankViewModel model)
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
        var owner = await userService.GetAllAsIqueriable().Where(m => m.UserName == model.OwnerUserName).FirstOrDefaultAsync();
        if (owner == null)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کاربر غیر مجاز",
                IsSuccess = false
            });
        }

        model.OwnerId = owner.Id.ToString();
        var result = await DiscountCodeBankService.EditDiscountCodeAsync(model);

        return Json(result);

    }

    [HttpPost]
    [Route("DeleteForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> DeleteForClient([FromBody] DiscountCodeBankViewModel model)
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


        var result = await DiscountCodeBankService.DeleteDiscountCodeAsync(model.Id);

        return Json(result);
    }

}
