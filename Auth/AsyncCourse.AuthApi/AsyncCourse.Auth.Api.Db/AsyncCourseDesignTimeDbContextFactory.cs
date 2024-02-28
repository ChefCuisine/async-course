using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Auth.Api.Db;

public class AsyncCourseDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<AuthApiDbContext, AuthApiDbContextCreator>
{
    protected override string MigrationDatabaseName { get; } = "Test";
}