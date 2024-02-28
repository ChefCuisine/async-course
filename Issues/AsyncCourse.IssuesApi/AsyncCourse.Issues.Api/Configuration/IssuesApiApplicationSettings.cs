using AsyncCourse.Core.Db.Configuration;

namespace AsyncCourse.Issues.Api.Configuration;

public class IssuesApiApplicationSettings : IAsyncCourseAppPropertiesWithDb
{
    public AsyncCourseDbConfiguration Db { get; set; }
}