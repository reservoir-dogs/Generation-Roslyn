using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;

namespace Generation.Interface
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static NamespaceDeclarationSyntax GetNamespace(this ClassDeclarationSyntax classDeclarationSyntax)
        {
            var result = (NamespaceDeclarationSyntax)classDeclarationSyntax.Parent;

            return result;
        }

        public static IEnumerable<MethodDeclarationSyntax> GetMethods(this ClassDeclarationSyntax classDeclarationSyntax)
        {
            var result = classDeclarationSyntax
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(metthod => IsPublic(metthod) && IsNonStatic(metthod));

            return result;
        }

        internal static bool IsPublic(MethodDeclarationSyntax metthod)
        {
            return metthod.Modifiers.Any(modifier => modifier.Kind() == SyntaxKind.PublicKeyword);
        }

        internal static bool IsNonStatic(MethodDeclarationSyntax metthod)
        {
            return !metthod.Modifiers.Any(modifier => modifier.Kind() == SyntaxKind.StaticKeyword);
        }
    }
}
