using Microsoft.EntityFrameworkCore;

namespace Initium.Api.Core.Database
{
    public interface IEntityTypeConfigurationProvider
    {
        void ApplyConfigurations(ModelBuilder modelBuilder);
    }
}