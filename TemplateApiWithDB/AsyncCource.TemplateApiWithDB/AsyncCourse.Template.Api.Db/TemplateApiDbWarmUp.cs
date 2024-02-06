using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Template.Api.Db;

public class TemplateApiDbWarmUp : DbWarmUpBase<TemplateApiDbContext>
{
    public TemplateApiDbWarmUp(
        IDbContextFactory<TemplateApiDbContext> dbContextFactory,
        IDbSettings settings)
        : base(dbContextFactory, settings)
    {
    }
}