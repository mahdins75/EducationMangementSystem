using System.Security.Claims;
using Implementation.AccessedLinkService;
using Implementation.UserService;
using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;
using DNTCaptcha.Core;
using Microsoft.Extensions.Options;
using Mstech.Wallet.ViewModel.DTO;
using System.Data;
using Implementation.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Implementation.TransactionServiceArea;

[Area("api/Notification")]
[Route("[area]/SMS")]
public class SMSController : SecureBaseController
{
    private readonly ModirPayamakService modirPayamakService;

    private readonly UserService userService;

    private readonly TransactionRequestService transactionRequestService;

    public SMSController(ModirPayamakService modirPayamakService, UserService userService, TransactionRequestService transactionRequestService)
    {
        this.modirPayamakService = modirPayamakService;
        this.userService = userService;
        this.transactionRequestService = transactionRequestService;
    }

    [HttpPost]
    [Route("SendConfirmationSMS")]
    [AllowAnonymous]
    public async Task<JsonResult> SendConfirmationSMS([FromBody] NeedToChangePasswordViewModel model)
    {
        var randome = new Random();
        var confirmationCode = randome.Next(1001, 9999);

        var user = await userService.GetAll(new UserViewModel() { UserName = model.UserName });

        var result = modirPayamakService.SendForgetPassCode(confirmationCode, model.UserName);

        return Json(new ResponseViewModel<NeedToChangePasswordViewModel>() { Entity = new NeedToChangePasswordViewModel(), IsSuccess = true });
    }
    [HttpPost]
    [Route("SendWithDrawalConfirmation")]
    [AllowAnonymous]
    public async Task<JsonResult> SendWithDrawalConfirmation([FromBody] WithDrawConfirmationViewModel model)
    {
        var randome = new Random();
        var confirmationCode = randome.Next(1001, 9999);

        var user = await userService.GetAll(new UserViewModel() { UserName = model.UserName });

        var transactionTequestResult = await transactionRequestService.CreateTransactionAsync(new TransactionRequestViewModel()
        {
            WalletId = model.WalletId,
            WalletOwnerUserName = model.UserName,
            WaitForConfirmation = true,
            ConfirmationCode = confirmationCode.ToString()

        });

        var result = modirPayamakService.SendForgetPassCode(confirmationCode, model.UserName);


        return Json(new ResponseViewModel<NeedToChangePasswordViewModel>() { Entity = new NeedToChangePasswordViewModel(), IsSuccess = true });
    }
}
