using System.Diagnostics;
using AsyncCourse.Auth.Api.Domain.Commands.Accounts;
using AsyncCourse.Auth.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using AsyncCourse.Auth.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AsyncCourse.Auth.Api.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IGetListCommand getListCommand;
    private readonly IGetCommand getCommand;
    private readonly IEditCommand editCommand;

    private const string AuthType = CookieAuthenticationDefaults.AuthenticationScheme;

    public HomeController(
        IGetListCommand getListCommand,
        IGetCommand getCommand,
        IEditCommand editCommand)
    {
        this.getListCommand = getListCommand;
        this.getCommand = getCommand;
        this.editCommand = editCommand;
    }

    // Показываем страницу со списком аккаунтов
    public async Task<IActionResult> Index()
    {
        var result = await getListCommand.GetListAsync();

        var mappedResult = result.Select(AccountMapper.MapFrom).ToArray();

        return View(mappedResult.ToArray());
    }

    // Показываем редактируемую страницу карточки
    public async Task<IActionResult> Open(Guid id)
    {
        var result = await getCommand.GetByIdAsync(id);
        if (result == null)
        {
            return RedirectToAction("Index");
        }

        return View(AccountMapper.MapFrom(result));
    }
    
    [HttpPost]
    public async Task<IActionResult> Save([FromForm] EditAccountModel editAccountModel)
    {
        await editCommand.EditAsync(AccountMapper.MapFromEditAccountModel(editAccountModel));

        return RedirectToAction("Index");
    }
    
    // todo Добавить возможность удаления (с отправкой CUD-deleted)

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(AuthType);
        return RedirectToAction("Login", "Access");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}