using DNTCaptcha.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace DNTCaptcha.TestWebApp.Controllers;

[Route("api/[controller]")]
[EnableCors("CorsPolicy")]
public class NgxController : Controller
{
    private readonly IDNTCaptchaApiProvider _apiProvider;

    public NgxController(IDNTCaptchaApiProvider apiProvider) => _apiProvider = apiProvider;



    [HttpGet("[action]")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    [EnableRateLimiting(DNTCaptchaRateLimiterPolicy.Name)] // don't forget this one!
    public ActionResult<DNTCaptchaApiResponse> CreateDNTCaptchaParams() =>
        // Note: For security reasons, a JavaScript client shouldn't be able to provide these attributes directly.
        // Otherwise an attacker will be able to change them and make them easier!
        _apiProvider.CreateDNTCaptcha(new DNTCaptchaTagHelperHtmlAttributes
        {
            BackColor = "#f7f3f3",
            FontName = "Tahoma",
            FontSize = 18,
            ForeColor = "#111111",
            Language = Language.English,
            DisplayMode = DisplayMode.SumOfTwoNumbers,
            Max = 90,
            Min = 1,
        });
}