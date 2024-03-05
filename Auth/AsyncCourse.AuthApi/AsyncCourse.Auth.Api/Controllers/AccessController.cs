using System.Security.Claims;
using AsyncCourse.Auth.Api.Domain.Commands.Accounts;
using AsyncCourse.Auth.Api.Mappers;
using AsyncCourse.Auth.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCourse.Auth.Api.Controllers;

public class AccessController : Controller
{
    private readonly IGetByLoginCommand getByLoginCommand;
    private readonly IAddCommand addCommand;
    
    private const string AuthType = CookieAuthenticationDefaults.AuthenticationScheme;

    public AccessController(
        IGetByLoginCommand getByLoginCommand,
        IAddCommand addCommand)
    {
        this.addCommand = addCommand;
        this.getByLoginCommand = getByLoginCommand;
    }

    public IActionResult Login()
    {
        var claimsPrincipal = HttpContext.User;

        if (claimsPrincipal.Identity != null && claimsPrincipal.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var existingAccount = await getByLoginCommand.GetByLoginAsync(loginModel.Email, loginModel.Password);
        if (existingAccount == null)
        {
            return RedirectToAction("Signup");
        }

        await SignInAsync(loginModel.Email, loginModel.KeepLoggedIn);

        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    public async Task<IActionResult> Signup(SignupModel signupModel)
    {
        var validationResult = GetValidationResult(signupModel);
        if (validationResult != null)
        {
            return validationResult;
        }

        await addCommand.AddAsync(AccountMapper.MapFromSignUpModel(signupModel));
        
        await SignInAsync(signupModel.Email, true);

        return RedirectToAction("Index", "Home");
    }

    private IActionResult GetValidationResult(SignupModel signupModel)
    {
        if (string.IsNullOrWhiteSpace(signupModel.Email))
        {
            ViewData["ValidateMessage"] = "email is absent";
            {
                return View();
            }
        }

        if (string.IsNullOrWhiteSpace(signupModel.Password))
        {
            ViewData["ValidateMessage"] = "password is absent";
            {
                return View();
            }
        }

        if (signupModel.Password != signupModel.RepeatedPassword)
        {
            ViewData["ValidateMessage"] = "passwords are different";
            {
                return View();
            }
        }

        return null;
    }

    private async Task SignInAsync(string email, bool keepLoggedIn)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, AuthType);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var properties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = keepLoggedIn
        };

        await HttpContext.SignInAsync(AuthType, claimsPrincipal, properties);
    }
}