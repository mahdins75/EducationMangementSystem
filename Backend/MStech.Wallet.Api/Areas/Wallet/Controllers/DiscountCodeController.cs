using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Implementation.DiscountCodeServiceArea;
using Implementation.UserService;
using Implementation.WalletClientArea;
using Microsoft.EntityFrameworkCore;

[Area("api/Wallet")]
[Route("[area]/DiscountCode")]
public class DiscountCodeController : SecureBaseController
{
    private readonly DiscountCodeService discountCodeService;
    private readonly DiscountCodeBankService discountCodeBankService;
    private readonly UserService userService;
    private readonly WalletClientService walletClientService;

    public DiscountCodeController(DiscountCodeService discountCodeService, DiscountCodeBankService discountCodeBankService, UserService userService, WalletClientService walletClientService)
    {
        this.discountCodeService = discountCodeService;
        this.discountCodeBankService = discountCodeBankService;
        this.walletClientService = walletClientService;
        this.userService = userService;

    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<JsonResult> GetAll([FromQuery] DiscountCodeViewModel model)
    {
        var result = await discountCodeService.GetAll(model);

        return Json(result);
    }

    [HttpPost]
    [Route("Create")]
    [AllowAnonymous]
    public async Task<JsonResult> Create([FromBody] DiscountCodeViewModel model)
    {
        var result = await discountCodeService.CreateDiscountCodeAsync(model);

        return Json(result);

    }

    [HttpPost]
    [Route("Update")]
    [AllowAnonymous]

    public async Task<JsonResult> Update([FromBody] DiscountCodeViewModel model)
    {
        var result = await discountCodeService.EditDiscountCodeAsync(model);

        return Json(result);

    }

    [HttpPost]
    [Route("Delete")]
    [AllowAnonymous]
    public async Task<JsonResult> Delete([FromBody] DiscountCodeViewModel model)
    {
        var result = await discountCodeService.DeleteDiscountCodeAsync(model.Id);

        return Json(result);
    }





    [HttpGet]
    [Route("GetAllForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> GetAllForClient([FromQuery] DiscountCodeViewModel model)
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

        //model.OwnerId = user.Entity.Any() ? user.Entity.FirstOrDefault().Id.ToString() : "";

        var result = await discountCodeService.GetAll(model);

        return Json(result);
    }


    [HttpPost]
    [Route("CreateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> CreateForClient([FromBody] DiscountCodeViewModel model)
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
        if (model.DiscountCodeBankId <= 0 && !string.IsNullOrEmpty(model.DiscountCodeText))
        {
            var discountCodebankId = await discountCodeBankService.GetAllAsIqueriable().Where(m => m.DiscountCodeText == model.DiscountCodeText).FirstOrDefaultAsync();

            if (discountCodebankId != null)
            {
                model.DiscountCodeBankId = discountCodebankId.Id;
            }

        }
        if (!string.IsNullOrEmpty(model.UserName))
        {
            var user = await userService.GetAllAsIqueriable().Where(m => m.UserName == model.UserName).FirstOrDefaultAsync();

            if (user != null)
            {
                model.UserId = user.Id;
            }

        }
        var result = await discountCodeService.CreateDiscountCodeAsync(model);
        return Json(result);

    }

    [HttpPost]
    [Route("UpdateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> UpdateForClient([FromBody] DiscountCodeViewModel model)
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


        var result = await discountCodeService.EditDiscountCodeAsync(model);

        return Json(result);

    }

    [HttpPost]
    [Route("DeleteForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> DeleteForClient([FromBody] DiscountCodeViewModel model)
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


        var result = await discountCodeService.DeleteDiscountCodeAsync(model.Id);

        return Json(result);
    }

}
