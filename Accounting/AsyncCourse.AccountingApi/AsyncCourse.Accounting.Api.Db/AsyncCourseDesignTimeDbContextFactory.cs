using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Accounting.Api.Db;

public class AsyncCourseDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<AccountingApiDbContext, AccountingApiDbContextCreator>
{
    protected override string MigrationDatabaseName { get; } = "accounting";
}