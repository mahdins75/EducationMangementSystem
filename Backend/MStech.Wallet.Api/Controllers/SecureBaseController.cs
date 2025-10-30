using Authorization.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[EnableCors("CorsPolicy")]
[SessionRequirement]
public abstract class SecureBaseController : Controller
{

}
