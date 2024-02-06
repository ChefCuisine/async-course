using System.Diagnostics;
using AsyncCource.TemplateApiWithDB.Models;
using AsyncCourse.Template.Api.Db;
using AsyncCourse.Template.Api.Db.Dbos;
using AsyncCourse.Template.Api.Models.TemplateDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncCource.TemplateApiWithDB.Controllers;

public class TemplateDomainModelController : Controller
{
    private readonly TemplateApiDbContext templateApiDbContext;

    public TemplateDomainModelController(
        AsyncCourse.Core.Db.DbContextSupport.IDbContextFactory<TemplateApiDbContext> contextFactory)
    {
        this.templateApiDbContext = contextFactory.CreateDbContext();
    }

    // Показываем страницу со списком
    public async Task<IActionResult> Index()
    {
        var result = await templateApiDbContext.TemplateDomainModelDbos.ToListAsync();
        var mappedResult = result.Select(dbo => new TemplateDomainModel
        {
            Id = dbo.Id,
            Name = dbo.Name,
            Surname = dbo.Surname
        }).ToArray();
        return View(mappedResult);
    }

    // Показываем страницу с добавлением
    public async Task<IActionResult> Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save(string name, string surname)
    {
        var result = await templateApiDbContext.AddAsync(new TemplateDomainModelDbo
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = surname
        });

        await templateApiDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}