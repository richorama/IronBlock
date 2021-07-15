using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
{
  [TestClass]
  public class ExampleTests
  {
    [TestMethod]
    public void Test_Example1()
    {
      var xml = File.ReadAllText("../../../Examples/example1.xml");

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic n; n = 1; for (int count = 0; count < 4; count++) { n = (n * 2); Console.WriteLine(n); }"));
    }


    [TestMethod]
    public void Test_Example2()
    {
      var xml = File.ReadAllText("../../../Examples/example2.xml");

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("if (((6 * 7) == 42)) { Console.WriteLine(\"Don't panic\"); } else { Console.WriteLine(\"Panic\"); }"));
    }

    [TestMethod]
    public void Test_Example3()
    {
      var xml = File.ReadAllText("../../../Examples/example3.xml");

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic a; dynamic b; dynamic i; void test(dynamic x) { a = 2; b = (a * b); b = (b / 2); if ((b > 6)) { for (i = 1; i <= 10; i += 2) { a = (a * i); b = (1 + x); } }  a = (b * x); }  test(11);"));
    }

    [TestMethod]
    public void Test_Example4()
    {
      var xml = File.ReadAllText("../../../Examples/example4.xml");

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

    }
  }
}
