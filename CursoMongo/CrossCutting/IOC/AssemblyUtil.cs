using System.Collections.Generic;
using System.Reflection;

namespace CrossCutting
{
    public class AssemblyUtil
    {
        /// <summary>
        /// Carrega todos os assemblys anotados abaixo, para podermos fazer a injeção de dependencia do autoMapper
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("CrossCutting"),
                Assembly.Load("Application"),
                Assembly.Load("Infra"),
            };
        }
    }
}