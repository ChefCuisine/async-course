using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Auth.Api.Db;

public class AuthApiDbWarmUp : DbWarmUpBase<AuthApiDbContext>
{
    public AuthApiDbWarmUp(
        IDbContextFactory<AuthApiDbContext> dbContextFactory,
        IDbSettings settings)
        : base(dbContextFactory, settings)
    {
    }
}