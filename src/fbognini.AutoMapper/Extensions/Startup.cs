using AutoMapper;
using fbognini.AutoMapper.Mappings;
using System;
using System.Linq;
using System.Reflection;

namespace fbognini.AutoMapper.Extensions
{
    public static class Startup
    {
        public static void AddCustomMappingProfile<TTypeMarker>(this IMapperConfigurationExpression config)
        {
            config.AddCustomMappingProfile(typeof(TTypeMarker));
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Type[] typeMarkers)
        {
            config.AddCustomMappingProfile(typeMarkers.Select(t => t.GetTypeInfo().Assembly).ToArray());
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

            var list = allTypes
                .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
                .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

            var profile = new CustomMappingProfile(list);

            config.AddProfile(profile);
        }
    }

}
