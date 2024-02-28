using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Issues.Api.Db;

public class IssuesApiDbWarmUp : DbWarmUpBase<IssuesApiDbContext>
{
    public IssuesApiDbWarmUp(
        IDbContextFactory<IssuesApiDbContext> dbContextFactory,
        IDbSettings settings)
        : base(dbContextFactory, settings)
    {
    }
}