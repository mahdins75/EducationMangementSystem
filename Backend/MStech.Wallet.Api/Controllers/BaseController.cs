using Authorization.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[SessionRequirement]
[AllowAnonymous]
public abstract class BaseController : Controller
{
    public BaseController()
    {
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
       
    }
}
