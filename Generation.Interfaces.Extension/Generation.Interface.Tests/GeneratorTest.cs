using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Xunit;

namespace Generation.Interface.Tests
{
    public class GeneratorTest
    {
        private readonly Generator target;

        public GeneratorTest()
        {
            target = new Generator();
        }

        [Fact]
        public void BuildCode_Empty_Success()
        {
            var content =
@"namespace Generation.Implementations
{
  public class Sample
  {
  }
}";

            var expected =
@"namespace Generation
{
  public interface ISample
  {
  }
}";

            var actual = target.BuildCode(GetFirstClass(content));

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildCode_VoidMethod_Success()
        {
            var content =
@"namespace Generation.Implementations
{
  public class Sample
  {
    public void Method() {}
  }
}";

            var expected =
@"namespace Generation
{
  public interface ISample
  {
    void Method();
  }
}";

            var actual = target.BuildCode(GetFirstClass(content));

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildCode_MethodWithReturnType_Success()
        {
            var content =
@"namespace Generation.Implementations
{
  public class Sample
  {
    public int Method() {}
  }
}";

            var expected =
@"namespace Generation
{
  public interface ISample
  {
    int Method();
  }
}";

            var actual = target.BuildCode(GetFirstClass(content));

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildCode_MethodWithOneParameter_Success()
        {
            var content =
@"namespace Generation.Implementations
{
  public class Sample
  {
    public int Method(int param) {}
  }
}";

            var expected =
@"namespace Generation
{
  public interface ISample
  {
    int Method(int param);
  }
}";

            var actual = target.BuildCode(GetFirstClass(content));

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildCode_InterfaceWithTwoMethods_Success()
        {
            var content =
@"namespace Generation.Implementations
{
  public class Sample
  {
    public void Method1() {}

    public int Method2() {}
  }
}";

            var expected =
@"namespace Generation
{
  public interface ISample
  {
    void Method1();

    int Method2();
  }
}";

            var actual = target.BuildCode(GetFirstClass(content));

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildCode_MethodWithTwoParameters_Success()
        {
            var content =
@"namespace Generation.Implementations
{
  public class Sample
  {
    public int Method2(int param1, double param2) {}
  }
}";

            var expected =
@"namespace Generation
{
  public interface ISample
  {
    int Method2(int param1, double param2);
  }
}";

            var actual = target.BuildCode(GetFirstClass(content));

            actual.Should().Be(expected);
        }

        private static ClassDeclarationSyntax GetFirstClass(string content)
        {
            var compilationUnitSyntax = CSharpSyntaxTree.ParseText(content).GetRoot() as CompilationUnitSyntax;
            var result = compilationUnitSyntax.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .FirstOrDefault();

            return result;
        }
    }
}
