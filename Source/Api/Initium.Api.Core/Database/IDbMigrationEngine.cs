using System.Collections.Generic;

namespace Initium.Api.Core.Database
{
    public interface IDbMigrationEngine
    {
        bool PerformMigration(IDictionary<string, string> variables);
    }
}