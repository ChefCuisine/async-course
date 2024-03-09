using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;

namespace AsyncCourse.Accounting.Api.Db;

public class AccountingApiDbWarmUp : DbWarmUpBase<AccountingApiDbContext>
{
    public AccountingApiDbWarmUp(
        IDbContextFactory<AccountingApiDbContext> dbContextFactory,
        IDbSettings settings)
        : base(dbContextFactory, settings)
    {
    }
}