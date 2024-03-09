using AsyncCourse.Auth.Api.Client;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.AccountingApi.Controllers;

public class AccessController : Controller
{
    private readonly IAuthApiClient authApiClient;

    private const string CookieName = ".AspNetCore.Cookies";

    public AccessController(IAuthApiClient authApiClient)
    {
        this.authApiClient = authApiClient;
    }

    public async Task<IActionResult> Login()
    {
        var cookies = HttpContext.Request.Cookies;
        if (cookies.TryGetValue(CookieName, out var aspNetCookie))
        {
            var requestCookie = $"{CookieName}={aspNetCookie}";
            var authApiResult = await authApiClient.Authenticated(requestCookie);

            if (authApiResult.IsSuccessful && authApiResult.Result)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        return BadRequest();
    }
}