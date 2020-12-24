using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Initium.Api.Core.Database
{
    public interface ISchemaIdentifier
    {
        string SelectedSchema => "dbo";

        Task<List<string>> SchemasInUse(CancellationToken cancellationToken = default)
        {
            var schemas = new List<string> { "dbo" };
            return Task.FromResult(schemas);
        }
    }
}