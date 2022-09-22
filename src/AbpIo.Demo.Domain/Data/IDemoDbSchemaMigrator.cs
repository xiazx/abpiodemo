using System.Threading.Tasks;

namespace AbpIo.Demo.Data;

public interface IDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
