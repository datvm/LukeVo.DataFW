using AutoMapper;
using LukeVo.DataFW.Data;
using LukeVo.DataFW.Data.Repositories;
using LukeVo.DataFW.Data.Services;
using LukeVo.DataFW.WebCore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LukeVo.DataFW.WebCore.DependencyInjection
{

    public static class DIExtensions
    {

        public static void AddRepositoriesPattern(this IServiceCollection services)
        {
            services.AddRepositoriesPattern(new DataFwDIOptions());
        }

        public static void AddRepositoriesPattern(this IServiceCollection services, Action<DataFwDIOptions> options)
        {
            var optionsObj = new DataFwDIOptions();
            options(optionsObj);

            services.AddRepositoriesPattern(optionsObj);
        }

        public static void AddRepositoriesPattern(this IServiceCollection services, DataFwDIOptions options)
        {
            // Collect types
            foreach (var assembly in options.Assemblies)
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    // Repository
                    if (type.IsClass && options.RepositoryNamespaces.Contains(type.Namespace))
                    {
                        var interfaces = type.ImplementedInterfaces;

                        foreach (var i in interfaces)
                        {
                            services.AddScoped(i, type);
                        }
                    }

                    // Services
                    if (type.IsClass && options.ServiceNamespaces.Contains(type.Namespace))
                    {
                        var interfaces = type.ImplementedInterfaces;

                        foreach (var i in interfaces)
                        {
                            services.AddScoped(i, type);
                        }
                    }
                }
            }

            // Unit of Work
            if (options.UnitOfWorkType != null)
            {
                services.AddScoped(typeof(IUnitOfWork), options.UnitOfWorkType);
            }

            if (options.UnitOfWorkAsyncType != null)
            {
                services.AddScoped(typeof(IUnitOfWorkAsync), options.UnitOfWorkAsyncType);
            }
            
            // Inject DbContexts
            foreach (var dbContextType in options.DataContextTypes)
            {
                if (!typeof(DbContext).IsAssignableFrom(dbContextType))
                {
                    throw new ArgumentException("The type provided is not subclass of DbContext: " + dbContextType.FullName);
                }

                services.AddScoped(typeof(DbContext), dbContextType);
            }
        }

        public static void AddViewModelAutoMapper(this IServiceCollection services)
        {
            services.AddViewModelAutoMapper(new Assembly[] { Assembly.GetEntryAssembly(), });
        }

        public static void AddViewModelAutoMapper(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var types = new List<KeyValuePair<Type, Type>>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    var currentType = type.BaseType;

                    while (currentType != null)
                    {
                        if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(DefaultEntityViewModel<>))
                        {
                            types.Add(new KeyValuePair<Type, Type>(type, currentType.GetGenericArguments()[0]));
                            break;
                        }

                        currentType = currentType.BaseType;
                    }
                }
            }

            Mapper.Initialize(config =>
            {
                foreach (var type in types)
                {
                    config.CreateMap(type.Key, type.Value)
                        .ReverseMap();
                }
            });
        }

        private static void AddIfTypeOf(this List<Type> list, Type masterType, Type definition, Type expected)
        {
            if (definition == expected)
            {
                list.Add(masterType);
            }
        }

        private static void AddIfTypeOf(this List<KeyValuePair<Type, Type>> list, Type masterType, Type implementedInterface, Type definition, Type expected)
        {
            if (definition == expected)
            {
                list.Add(new KeyValuePair<Type, Type>(implementedInterface, masterType));
            }
        }

    }

}
