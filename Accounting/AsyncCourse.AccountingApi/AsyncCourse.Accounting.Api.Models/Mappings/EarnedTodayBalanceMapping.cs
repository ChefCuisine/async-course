using AsyncCourse.Accounting.Api.Models.Analytics;
using AsyncCourse.Accounting.Api.Models.Analytics.Models;

namespace AsyncCourse.Accounting.Api.Models.Mappings;

public static class EarnedTodayBalanceMapping
{
    public static EarnedTodayBalanceModel MapFromDomainResult(EarnedTodayBalance balance)
    {
        return new EarnedTodayBalanceModel
        {
            PopugsWithNegativeBalance = balance.PopugsWithNegativeBalance,
            ManagementEarnedToday = balance.ManagementEarnedToday
        };
    }
}