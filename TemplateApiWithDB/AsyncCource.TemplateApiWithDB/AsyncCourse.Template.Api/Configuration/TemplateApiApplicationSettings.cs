using AsyncCourse.Core.Db.Configuration;

namespace AsyncCource.TemplateApiWithDB.Configuration;

public class TemplateApiApplicationSettings : IAsyncCourseAppPropertiesWithDb
{
    public AsyncCourseDbConfiguration Db { get; set; }
}