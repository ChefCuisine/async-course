using System.Diagnostics;
using AsyncCource.TemplateApiWithDB.Models;
using AsyncCourse.Template.Api.Domain.Commands.TemplateDomain.Add;
using AsyncCourse.Template.Api.Domain.Commands.TemplateDomain.List;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCource.TemplateApiWithDB.Controllers;

public class TemplateDomainModelController : Controller
{
    private readonly IGetListCommand getListCommand;
    private readonly IAddCommand addCommand;

    public TemplateDomainModelController(
        IGetListCommand getListCommand,
        IAddCommand addCommand)
    {
        this.getListCommand = getListCommand;
        this.addCommand = addCommand;
    }

    // Показываем страницу со списком
    public async Task<IActionResult> Index()
    {
        var result = await getListCommand.GetListAsync();

        return View(result.ToArray());
    }

    // Показываем страницу с добавлением
    public async Task<IActionResult> Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save(string name, string surname)
    {
        await addCommand.AddAsync(name, surname);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}