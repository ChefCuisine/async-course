using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Template.Api.Db;

public class TemplateApiDbContextCreator : IDbContextCreator<TemplateApiDbContext>
{
    public TemplateApiDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new TemplateApiDbContext(settings, loggerFactory);
    }
}