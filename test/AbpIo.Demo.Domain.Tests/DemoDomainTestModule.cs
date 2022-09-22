using AbpIo.Demo.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpIo.Demo;

[DependsOn(
    typeof(DemoEntityFrameworkCoreTestModule)
    )]
public class DemoDomainTestModule : AbpModule
{

}
