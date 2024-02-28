using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Issues.Api.Db;

public class IssuesApiDbContextCreator : IDbContextCreator<IssuesApiDbContext>
{
    public IssuesApiDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new IssuesApiDbContext(settings, loggerFactory);
    }
}