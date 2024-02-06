using Npgsql;

namespace AsyncCourse.Core.Db.Configuration;

public class AsyncCourseDbSettings<TAppProperties> : IDbSettings
    where TAppProperties: IAsyncCourseAppPropertiesWithDb
{
    private readonly TAppProperties appProperties;

    public AsyncCourseDbSettings(TAppProperties appProperties)
    {
        this.appProperties = appProperties;
    }

    public string ConnectionString
    {
        get
        {
            var dbConfiguration = appProperties.Db;
            var builder = new NpgsqlConnectionStringBuilder(dbConfiguration.ConnectionString)
            {
                Username = dbConfiguration.User,
                Password = dbConfiguration.Password
            };
            return builder.ToString();
        }
    }
    
    public bool DisableMigrations => false;
}