using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Topology;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Client;

public abstract class RootClientBase : ClientBase
{
    protected RootClientBase(Uri uri, ILog log)
        : this(
            s =>
            {
                s.ClusterProvider = new FixedClusterProvider(uri);
            },
            log)
    {
    }
    
    protected RootClientBase(ClusterClientSetup setup, ILog log): base()
    {
        ClusterClient = new ClusterClient(log.ForContext(GetType()), setup);
    }
}