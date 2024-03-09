using AsyncCourse.Core.Db.Configuration;

namespace AsyncCourse.AccountingApi.Configuration;

public class AccountingApiApplicationSettings : IAsyncCourseAppPropertiesWithDb
{
    public AsyncCourseDbConfiguration Db { get; set; }
}