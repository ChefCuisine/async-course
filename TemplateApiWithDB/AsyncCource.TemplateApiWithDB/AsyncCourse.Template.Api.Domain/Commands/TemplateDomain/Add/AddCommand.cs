using AsyncCourse.Core.Db.DbContextSupport;
using AsyncCourse.Template.Api.Db;
using AsyncCourse.Template.Api.Db.Dbos;

namespace AsyncCourse.Template.Api.Domain.Commands.TemplateDomain.Add;

public interface IAddCommand
{
    Task AddAsync(string name, string surname);
}

public class AddCommand : IAddCommand
{
    private readonly TemplateApiDbContext templateApiDbContext;

    public AddCommand(IDbContextFactory<TemplateApiDbContext> contextFactory)
    {
        templateApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task AddAsync(string name, string surname)
    {
        await templateApiDbContext.AddAsync(new TemplateDomainModelDbo
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = surname
        });

        await templateApiDbContext.SaveChangesAsync();
    }
}