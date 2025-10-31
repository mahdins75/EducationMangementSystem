using Implementation.AccessLinkeRoles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Route("[area]/Home")]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class HomeController : BaseController
{

    
    public HomeController()
    {
    }

    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    [Route("Amin")]
    public async Task<IActionResult> Amin()
    {
        return View();
    }

}
