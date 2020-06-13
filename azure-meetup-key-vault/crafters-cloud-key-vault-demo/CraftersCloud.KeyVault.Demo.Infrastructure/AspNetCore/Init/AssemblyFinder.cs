using System.Reflection;

namespace CraftersCloud.KeyVault.Demo.Infrastructure.AspNetCore.Init
{
    public static class AssemblyFinder
    {
        private const string ProjectPrefix = "CraftersCloud.KeyVault.Demo";

        public static Assembly ApiAssembly => FindAssembly("Api");

        private static Assembly FindAssembly(string projectSuffix)
        {
            return Assembly.Load($"{ProjectPrefix}.{projectSuffix}");
        }
    }
}