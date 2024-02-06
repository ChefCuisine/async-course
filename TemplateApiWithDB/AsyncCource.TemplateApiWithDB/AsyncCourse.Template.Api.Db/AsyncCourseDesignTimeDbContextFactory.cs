using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Template.Api.Db;

public class AsyncCourseDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<TemplateApiDbContext, TemplateApiDbContextCreator>
{
    protected override string MigrationDatabaseName { get; } = "Test";
}