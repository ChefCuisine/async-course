using System.Diagnostics;
using AsyncCourse.Issues.Api.Domain.Commands.Issues;
using Microsoft.AspNetCore.Mvc;
using AsyncCourse.Issues.Api.Models;
using AsyncCourse.Issues.Api.Models.Issues.Models;
using AsyncCourse.Issues.Api.Models.Mappers;
using AsyncCourse.Issues.Api.Models.Models;

namespace AsyncCourse.Issues.Api.Controllers;

public class HomeController : Controller
{
    private readonly IGetListCommand getListCommand;
    private readonly IAddCommand addCommand;
    private readonly IGetCommand getCommand;
    private readonly IReassignCommand reassignCommand;

    public HomeController(
        IGetListCommand getListCommand,
        IAddCommand addCommand,
        IGetCommand getCommand,
        IReassignCommand reassignCommand)
    {
        this.getListCommand = getListCommand;
        this.addCommand = addCommand;
        this.getCommand = getCommand;
        this.reassignCommand = reassignCommand;
    }

    // Показываем страницу со списком задач
    public async Task<IActionResult> Index()
    {
        var result = await getListCommand.GetListAsync();

        return View(result.ToArray());
    }
    
    // Показываем страницу с добавлением
    public IActionResult Add()
    {
        return View();
    }   

    [HttpPost]
    public async Task<IActionResult> Save([FromForm] IssueModel issueModel)
    {
        await addCommand.AddAsync(IssueMapper.MapFromIssueModel(issueModel));

        return RedirectToAction("Index");
    }
    
    // Показываем редактируемую страницу карточки
    public async Task<IActionResult> Open(Guid id)
    {
        var result = await getCommand.GetAsync(id);
        if (result == null)
        {
            return RedirectToAction("Index");
        }

        return View(IssueMapper.MapFrom(result));
    }

    public async Task Reassign()
    {
        await reassignCommand.Reassign();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}