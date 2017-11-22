using System.IO;
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

            Assert.AreEqual("24816", printer.Text);
        }


        [TestMethod]
        public void Test_Example2()
        {
            var xml = File.ReadAllText("../../../Examples/example2.xml");
            var parser = new Parser();

            parser.AddStandardBlocks();
            var printer = parser.AddDebugPrinter();

            parser.Parse(xml).Evaluate();

            Assert.AreEqual("Don't panic", printer.Text);
        }





    }


}
