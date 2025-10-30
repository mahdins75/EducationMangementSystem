using Implementation.AccessedLinkService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Menue")] // Specify the area name
[Route("Menue/Account")]
[Authorize]
public class MenueItemsController : Controller
{

    private readonly AccessedLinkService _accessedLinkService;
    public MenueItemsController(AccessedLinkService accessedLinkService)
    {
        _accessedLinkService = accessedLinkService;
    }
}
