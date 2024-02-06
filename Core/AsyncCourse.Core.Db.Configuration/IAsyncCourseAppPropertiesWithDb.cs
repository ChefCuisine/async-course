namespace AsyncCourse.Core.Db.Configuration;

public interface IAsyncCourseAppPropertiesWithDb
{
    AsyncCourseDbConfiguration Db { get; set; }
}