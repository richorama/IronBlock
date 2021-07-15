using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  [TestClass]
  public class ExampleTests
  {
    [TestMethod]
    public void Test_Example1()
    {
      var xml = File.ReadAllText("../../../Examples/example1.xml");
      var parser = new Parser();

      parser.AddStandardBlocks();
      var printer = parser.AddDebugPrinter();

      parser.Parse(xml).Evaluate();

      Assert.AreEqual("2,4,8,16", string.Join(",", TestExtensions.GetDebugText()));
    }


    [TestMethod]
    public void Test_Example2()
    {
      var xml = File.ReadAllText("../../../Examples/example2.xml");
      var parser = new Parser();

      parser.AddStandardBlocks();
      var printer = parser.AddDebugPrinter();

      parser.Parse(xml).Evaluate();

      Assert.AreEqual("Don't panic", TestExtensions.GetDebugText().First());
    }





  }


}
