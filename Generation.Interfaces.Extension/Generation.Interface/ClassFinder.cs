using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

namespace Generation.Interface
{
    public interface IClassFinder
    {
        IEnumerable<ClassDeclarationSyntax> GetClasses();
    }
}
