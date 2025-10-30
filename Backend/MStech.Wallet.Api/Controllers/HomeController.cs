using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mstech.ADV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Mstech.ADV.Controllers;
[AllowAnonymous]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly ControllerActionDiscoveryHostedService controllerActionDiscoveryHostedService;



    public HomeController(ILogger<HomeController> logger, ControllerActionDiscoveryHostedService controllerActionDiscoveryHostedService)
    {
        _logger = logger;
        this.controllerActionDiscoveryHostedService = controllerActionDiscoveryHostedService;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Landing()
    {

        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult EmailTemplate()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
