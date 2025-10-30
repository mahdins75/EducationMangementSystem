using Microsoft.AspNetCore.Mvc;
using Mstech.ViewModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Implementation.TransactionServiceArea;
using Implementation.WalletClientArea;
using Common.Gaurd;
using Implementation.UserService;
using MStech.Wallet.ViewModel.ViewModel.Wallet;

[Area("api/Wallet")]
[Route("[area]/Transaction")]
public class TransactionController : SecureBaseController
{
    private readonly TransactionService TransactionClientService;
    private readonly TransactionRequestService transactionRequestService;
    private readonly WalletClientService walletClientService;
    private readonly UserService userService;

    public TransactionController(TransactionService TransactionClientService, WalletClientService walletClientService, UserService userService, TransactionRequestService transactionRequestService)
    {
        this.TransactionClientService = TransactionClientService;
        this.walletClientService = walletClientService;
        this.userService = userService;
        this.transactionRequestService = transactionRequestService;
    }

    [HttpGet]
    [HttpOptions]
    [Route("GetAll")]
    public async Task<JsonResult> GetAll([FromQuery] TransactionViewModel model)
    {
        var result = await TransactionClientService.GetAll(model);

        return Json(result);
    }
    [HttpGet]
    [HttpOptions]
    [Route("GetAllInvoices")]
    public async Task<JsonResult> GetAllInvoices([FromQuery] TransactionViewModel model)
    {
        var result = await TransactionClientService.GetAllInvoices(model);

        return Json(result);
    }

    [HttpGet]
    [HttpOptions]
    [Route("GetAllOfMyInvoices")]
    public async Task<JsonResult> GetAllOfMyInvoices([FromQuery] TransactionViewModel model)
    {
        var userName = User.Identity.Name;
        var userClients = await walletClientService.GetAllWithApiId(new WalletClientViewModel() { OwnerUserName = userName });

        if (!userClients.Success || userClients.Entity == null)
        {
            return Json(new ResponseViewModel<List<GetAllOfMyClientsViewModel>>() { Entity = new List<GetAllOfMyClientsViewModel>(), IsSuccess = false });
        }
        if (!userClients.Entity.Any(m => m.OwnerUserName == userName))
        {
            return Json(new ResponseViewModel<List<GetAllOfMyClientsViewModel>>() { Entity = new List<GetAllOfMyClientsViewModel>(), IsSuccess = false });
        }
        model.ClientApiId = userClients.Entity.FirstOrDefault(m => m.OwnerUserName == userName).ClientIdForApi;
        var result = await TransactionClientService.GetAllOfMyInvoices(model);

        return Json(result);
    }

    [HttpGet]
    [HttpOptions]
    [Route("GetAllOfMyUsers")]
    public async Task<JsonResult> GetAllOfMyUsers([FromQuery] TransactionViewModel model)
    {
        var userName = User.Identity.Name;
        var userClients = await walletClientService.GetAllWithApiId(new WalletClientViewModel() { OwnerUserName = userName });

        if (!userClients.Success || userClients.Entity == null)
        {
            return Json(new ResponseViewModel<List<GetAllOfMyClientsViewModel>>() { Entity = new List<GetAllOfMyClientsViewModel>(), IsSuccess = false });
        }
        if (!userClients.Entity.Any(m => m.OwnerUserName == userName))
        {
            return Json(new ResponseViewModel<List<GetAllOfMyClientsViewModel>>() { Entity = new List<GetAllOfMyClientsViewModel>(), IsSuccess = false });
        }
        model.ClientApiId = userClients.Entity.FirstOrDefault(m => m.OwnerUserName == userName).ClientIdForApi;
        var result = await TransactionClientService.GetAllOfMyUsers(model);
        return Json(result);
    }
    [HttpGet]
    [HttpOptions]
    [Route("GetBalance")]
    public async Task<JsonResult> GetBalance([FromQuery] int walletId)
    {
        var result = await TransactionClientService.GetBalance(walletId);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("Create")]
    public async Task<JsonResult> Create([FromBody] TransactionViewModel model)
    {
        var result = await TransactionClientService.CreateTransactionAsync(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("CreateDepositForClientOwner")]
    public async Task<JsonResult> CreateDepositForClientOwner([FromBody] TransactionViewModel model)
    {
        var result = await TransactionClientService.CreateTransactionAsync(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("Transfer")]
    public async Task<JsonResult> Transfer([FromBody] TransferTransactionViewModel model)
    {
        var result = await TransactionClientService.Transfer(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("WithDrawal")]
    public async Task<JsonResult> WithDrawal([FromBody] WithDrawConfirmationViewModel model)
    {
        var userName = User.Identity.Name;
        var result = await transactionRequestService.ConfirmTransactionRequestAsync(new TransactionRequestViewModel()
        {
            Id = model.TransactionRequestId.Value,
            ConfirmationCode = model.ConfirmationCode,
            WalletOwnerUserName = model.UserName
        });


        return Json(result);
    }

    [HttpGet]
    [HttpOptions]
    [Route("SumOfAllRequestAmounts")]
    public async Task<JsonResult> SumOfAllRequestAmounts()
    {
        var userName = User.Identity.Name;

        var result = await transactionRequestService.SumOfAllRequestAmountsAynce();

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("Update")]
    public async Task<JsonResult> Update([FromBody] TransactionViewModel model)
    {
        var result = await TransactionClientService.EditTransactionAsync(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("Delete")]
    public async Task<JsonResult> Delete([FromBody] TransactionViewModel model)
    {
        var result = await TransactionClientService.DeleteTransactionAsync(model.Id);

        return Json(result);
    }

    ////client side
    ///

    [HttpGet]
    [HttpOptions]
    [Route("GetAllForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> GetAllForClient([FromQuery] TransactionViewModel model)
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

        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName, UserId = "" }).Result.Entity.Any();

        if (!clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var result = await TransactionClientService.GetAll(model);

        return Json(result);
    }
    [HttpGet]
    [HttpOptions]
    [Route("GetReportForInvoiceForClinet")]
    [AllowAnonymous]
    public async Task<JsonResult> GetReportForInvoiceForClinet([FromQuery] TransactionViewModel model)
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

        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName, UserId = "" }).Result.Entity.Any();

        if (!clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var result = await TransactionClientService.GetReportForInvoice(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("CreateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> CreateForClient([FromBody] TransactionViewModel model)
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

        var result = await TransactionClientService.CreateTransactionAsync(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("CreateRangeForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> CreateRangeForClient([FromBody] List<TransactionViewModel> model)
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

        var clientsAccessStatus = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserName = userName, UserId = "" }).Result.Entity.Any();

        if (!clientsAccessStatus)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        var result = await TransactionClientService.CreateRangeTransactionAsync(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("UpdateForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> UpdateForClient([FromBody] TransactionViewModel model)
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

        if (Request.Headers.TryGetValue("clientidforapis", out var userName))
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

        var result = await TransactionClientService.EditTransactionAsync(model);

        return Json(result);
    }

    [HttpPost]
    [HttpOptions]
    [Route("DeleteForClient")]
    [AllowAnonymous]
    public async Task<JsonResult> DeleteForClient([FromBody] TransactionViewModel model)
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

        if (Request.Headers.TryGetValue("clientidforapis", out var userName))
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

        var result = await TransactionClientService.DeleteTransactionAsync(model.Id);

        return Json(result);
    }

    [HttpGet]
    [HttpOptions]
    [Route("GetBalanceForClient")]
    public async Task<JsonResult> GetBalanceForClient([FromQuery] TransactionViewModel model)
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

        if (Request.Headers.TryGetValue("clientidforapis", out var userName))
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

        var result = await TransactionClientService.GetBalance(model.WalletId.Value);

        return Json(result);
    }

}
