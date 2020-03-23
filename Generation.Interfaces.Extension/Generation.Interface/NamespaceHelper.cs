using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;

namespace Generation.Interface
{
    public static class NamespaceHelper
    {
        public static string GetTarget(string namespaceName)
        {
            var parts = namespaceName.Split('.');

            var result = string.Join(".", parts.Take(parts.Length - 1));

            return result;
        }
    }
}
