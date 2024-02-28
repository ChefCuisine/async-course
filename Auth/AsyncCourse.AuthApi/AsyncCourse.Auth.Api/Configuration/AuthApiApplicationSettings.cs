using AsyncCourse.Core.Db.Configuration;

namespace AsyncCourse.Auth.Api.Configuration;

public class AuthApiApplicationSettings : IAsyncCourseAppPropertiesWithDb
{
    public AsyncCourseDbConfiguration Db { get; set; }
}