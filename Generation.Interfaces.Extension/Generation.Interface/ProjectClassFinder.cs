using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;

namespace Generation.Interface
{
    public class ProjectClassFinder : IClassFinder
    {
        private readonly Project project;

        public ProjectClassFinder(Project project)
        {
            this.project = project;
        }

        public IEnumerable<ClassDeclarationSyntax> GetClasses()
        {
            var result = project.Documents
                .SelectMany(document =>
                {
                    var tree = document.GetSyntaxTreeAsync().Result;

                    var classes = tree.GetRoot()
                      .DescendantNodes()
                      .OfType<ClassDeclarationSyntax>();

                    return classes;
                });

            return result;
        }
    }
}
