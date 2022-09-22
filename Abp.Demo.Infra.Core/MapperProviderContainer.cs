using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.DependencyInjection;


namespace Abp.Demo.Infra.Core
{
    public class MapperProviderContainer
    {
        private static readonly IConfigurationProviderContainer providerContainer;
        static MapperProviderContainer()
        {
            providerContainer = ServiceContainer.ServiceProvider.GetService<IConfigurationProviderContainer>();
        }

        public static IConfigurationProvider Configuration => providerContainer.Configuration;
    }

    public interface IConfigurationProviderContainer
    {
        IConfigurationProvider Configuration { get; }
    }


    //public class NullConfigurationProviderContainer : IConfigurationProviderContainer, ISingletonDependency
    //{
    //    IConfigurationProvider IConfigurationProviderContainer.Configuration => default;
    //}

    //[Dependency(ReplaceServices = true)]
    public class DefaultConfigurationProviderContainer : IConfigurationProviderContainer, ISingletonDependency
    {
        private readonly IMapperAccessor _mapperAccessor;

        public DefaultConfigurationProviderContainer(IMapperAccessor mapperAccessor)
        {
            _mapperAccessor = mapperAccessor;
        }

        public IConfigurationProvider Configuration => _mapperAccessor.Mapper.ConfigurationProvider;
    }

    public static class QueryableExtension
    {
        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source)
        {
           return source.ProjectTo<TDestination>(MapperProviderContainer.Configuration);
        }
    }
}
