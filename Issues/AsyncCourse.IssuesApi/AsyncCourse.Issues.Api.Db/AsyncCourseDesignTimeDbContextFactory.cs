using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Issues.Api.Db;

public class AsyncCourseDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<IssuesApiDbContext, IssuesApiDbContextCreator>
{
    protected override string MigrationDatabaseName { get; } = "issues";
}