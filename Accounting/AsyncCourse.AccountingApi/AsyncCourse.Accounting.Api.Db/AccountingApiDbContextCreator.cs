using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.DbContextSupport;
using Microsoft.Extensions.Logging;

namespace AsyncCourse.Accounting.Api.Db;

public class AccountingApiDbContextCreator : IDbContextCreator<AccountingApiDbContext>
{
    public AccountingApiDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new AccountingApiDbContext(settings, loggerFactory);
    }
}