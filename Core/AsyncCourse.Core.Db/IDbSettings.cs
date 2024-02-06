using JetBrains.Annotations;

namespace AsyncCourse.Core.Db;

public interface IDbSettings
{
    [NotNull]
    string ConnectionString { get; }
    
    bool DisableMigrations { get; }
}