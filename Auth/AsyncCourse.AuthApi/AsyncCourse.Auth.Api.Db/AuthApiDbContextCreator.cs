using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Auth.Api.Db;

public class AuthApiDbContextCreator : IDbContextCreator<AuthApiDbContext>
{
    public AuthApiDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new AuthApiDbContext(settings, loggerFactory);
    }
}