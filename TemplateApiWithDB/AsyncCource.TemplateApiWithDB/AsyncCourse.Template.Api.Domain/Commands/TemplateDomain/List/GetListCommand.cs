using AsyncCourse.Template.Api.Db;
using AsyncCourse.Template.Api.Domain.Models.TemplateDomain;
using Microsoft.EntityFrameworkCore;

namespace AsyncCourse.Template.Api.Domain.Commands.TemplateDomain.List;

public interface IGetListCommand
{
    Task<List<TemplateDomainModel>> GetListAsync();
}

public class GetListCommand : IGetListCommand
{
    private readonly TemplateApiDbContext templateApiDbContext;

    public GetListCommand(
        Core.Db.DbContextSupport.IDbContextFactory<TemplateApiDbContext> contextFactory)
    {
        templateApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task<List<TemplateDomainModel>> GetListAsync()
    {
        var result = await templateApiDbContext.TemplateDomainModelDbos.ToListAsync();
        var mappedResult = result.Select(dbo => new TemplateDomainModel
        {
            Id = dbo.Id,
            Name = dbo.Name,
            Surname = dbo.Surname
        }).ToList();

        return mappedResult;
    }
}