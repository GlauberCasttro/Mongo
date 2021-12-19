using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace API.POC_MONGO.CrossCutting.Assemblies
{
    [ExcludeFromCodeCoverage]
    public class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {            
            return new Assembly[]
            {
                Assembly.Load("API.POC_MONGO.Api"),
                Assembly.Load("API.POC_MONGO.Application"),
                Assembly.Load("API.POC_MONGO.Domain"),
                Assembly.Load("API.POC_MONGO.Domain.Core"),
                Assembly.Load("API.POC_MONGO.Infrastructure"),
                Assembly.Load("API.POC_MONGO.CrossCutting")
            };
        }
    }
}
