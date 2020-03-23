using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;

namespace Generation.Interface
{
    public static class MethodDeclarationSyntaxExtensions
    {
        public static IEnumerable<ParameterSyntax> GetParameters(this MethodDeclarationSyntax methodDeclarationSyntax)
        {
            var result = methodDeclarationSyntax.ParameterList.Parameters
                .OfType<ParameterSyntax>();

            return result;
        }

    }
}
