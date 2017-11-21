using System.Collections.Generic;
using System.IO;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Example1()
        {
            var xml = File.ReadAllText("../../../Examples/example1.xml");
            var parser = new Parser();

            parser.AddStandardBlocks();

            var workspace = parser.Parse(xml);

            var output = workspace.Evaluate(new Dictionary<string,object>());
        }


        [TestMethod]
        public void Test_Example2()
        {
            var xml = File.ReadAllText("../../../Examples/example2.xml");
            var parser = new Parser();

            parser.AddStandardBlocks();

            var workspace = parser.Parse(xml);

            var output = workspace.Evaluate(new Dictionary<string,object>());
        }





    }


}
