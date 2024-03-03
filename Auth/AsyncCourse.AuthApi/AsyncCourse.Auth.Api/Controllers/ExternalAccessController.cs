using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.Auth.Api.Controllers;

[Authorize]
[Route("external-access")]
public class ExternalAccessController : Controller
{
    [HttpGet("login")]
    public bool Login()
    {
        var claimsPrincipal = HttpContext.User;

        if (claimsPrincipal.Identity != null && claimsPrincipal.Identity.IsAuthenticated)
        {
            return true;
        }
        
        return false;
    }
}