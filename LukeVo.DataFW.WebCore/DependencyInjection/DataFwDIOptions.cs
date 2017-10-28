using LukeVo.DataFW.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LukeVo.DataFW.WebCore.DependencyInjection
{

    public class DataFwDIOptions
    {

        public List<Assembly> Assemblies { get; set; } = new List<Assembly>()
        {
            Assembly.GetEntryAssembly(),
        };
        
        public List<Type> DataContextTypes { get; set; } = new List<Type>();

        public HashSet<string> RepositoryNamespaces { get; set; } = new HashSet<string>();
        public HashSet<string> ServiceNamespaces { get; set; } = new HashSet<string>();

        public Type UnitOfWorkType { get; set; } = typeof(DefaultUnitOfWork);
        public Type UnitOfWorkAsyncType { get; set; } = typeof(DefaultUnitOfWorkAsync);
        
    }

}
