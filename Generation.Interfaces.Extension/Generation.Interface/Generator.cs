using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Generation.Interface.Tests")]
namespace Generation.Interface
{
    public class Generator
    {
        public string BuildCode(ClassDeclarationSyntax classDeclarationSyntax)
        {
            var result = new StringBuilder();

            BuildCodeNamespace(classDeclarationSyntax, result);

            return result.ToString();
        }

        internal void BuildCodeNamespace(ClassDeclarationSyntax classDeclarationSyntax, StringBuilder result)
        {
            var namespaceTarget = NamespaceHelper.GetTarget(classDeclarationSyntax.GetNamespace().Name.ToString());

            result.AppendLine($"namespace {namespaceTarget}");
            result.AppendLine("{");

            BuildCodeInterface(classDeclarationSyntax, result);

            result.Append("}");
        }

        internal void BuildCodeInterface(ClassDeclarationSyntax classDeclarationSyntax, StringBuilder result)
        {
            result.AppendLine($"  public interface I{classDeclarationSyntax.Identifier.Text}");
            result.AppendLine("  {");

            var methodDeclarationSyntaxes = classDeclarationSyntax.GetMethods();

            var methodDeclarationSyntaxeInfos = methodDeclarationSyntaxes
                .ToList()
                .Select((item, index) => new { Item = item, IsLast = index == methodDeclarationSyntaxes.Count() - 1 });

            foreach (var methodDeclarationSyntaxInfo in methodDeclarationSyntaxeInfos)
                BuildCodeMethod(methodDeclarationSyntaxInfo.Item, methodDeclarationSyntaxInfo.IsLast, result);

            result.AppendLine("  }");
        }

        internal void BuildCodeMethod(MethodDeclarationSyntax methodDeclarationSyntax, bool isLast, StringBuilder stringBuilder)
        {
            stringBuilder.Append($"    {methodDeclarationSyntax.ReturnType.GetText()}{methodDeclarationSyntax.Identifier.Text}");
            stringBuilder.Append("(");

            var parameterSyntaxes = methodDeclarationSyntax.GetParameters();

            var start = false;
            foreach (var parameterSyntax in parameterSyntaxes)
            {
                if (start)
                    stringBuilder.Append(", ");

                BuildCodeParameter(parameterSyntax, stringBuilder);
                start = true;
            }

            stringBuilder.AppendLine(");");
            if (!isLast)
                stringBuilder.AppendLine(string.Empty);
        }

        internal void BuildCodeParameter(ParameterSyntax parameterSyntax, StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{parameterSyntax.Type.ToString()} {parameterSyntax.Identifier.Text}");
        }
    }
}
