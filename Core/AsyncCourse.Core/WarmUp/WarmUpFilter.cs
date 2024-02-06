using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace AsyncCourse.Core.WarmUp;

public class WarmUpFilter : IStartupFilter
{
    private readonly IEnumerable<IWarmUp> warmUps;

    public WarmUpFilter(IEnumerable<IWarmUp> warmUps)
    {
        this.warmUps = warmUps;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        foreach (var warmUp in warmUps)
        {
            warmUp.RunAsync().GetAwaiter().GetResult();
        }
   
        return next;
    }
}