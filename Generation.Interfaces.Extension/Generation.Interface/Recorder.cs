using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.IO;

namespace Generation.Interface
{
    public class Recorder
    {
        private readonly ProjectFinder projectFinder;
        private readonly Generator generator;

        public Recorder(ProjectFinder projectFinder, Generator generator)
        {
            this.projectFinder = projectFinder;
            this.generator = generator;
        }

        public void Save()
        {
            foreach (var project in projectFinder.GetProjects())
            {
                SaveClasses(project);
            }
        }

        private void SaveClasses(Project project)
        {
            var analyzer = new ProjectClassFinder(project);

            foreach (var classDeclarationSyntax in analyzer.GetClasses())
            {
                var code = generator.BuildCode(classDeclarationSyntax);
                var path = GetPath(project, classDeclarationSyntax);

                Save(code, path);
            }
        }

        private void Save(string code, string path)
        {
            var directory = Directory.GetParent(path).FullName;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(path, code);
        }

        private string GetPath(Project project, ClassDeclarationSyntax classDeclarationSyntax)
        {
            var projectDirectoryPath = Path.GetDirectoryName(project.FilePath).Replace(".Implementations", string.Empty);
            var namespaceDeclarationSyntax = classDeclarationSyntax.GetNamespace();
            var subDirectoryPath = namespaceDeclarationSyntax.Name.ToString().Replace($"{project.AssemblyName}.", string.Empty).Replace(".", @"\");

            var result = Path.Combine(projectDirectoryPath, subDirectoryPath, $"I{classDeclarationSyntax.Identifier.ValueText}.cs");

            return result;
        }
    }
}
