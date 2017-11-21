using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Test_Mutations()
        {
            const string xml = @"
<xml>
    <block type=""controls_if"">
        <mutation else=""1""/>
    </block>            
</xml>
";

            var parser = new Parser();
            parser.AddStandardBlocks();

            var workspace = parser.Parse(xml);

            Assert.AreEqual(1, workspace.Blocks.Count);
            Assert.AreEqual(1, workspace.Blocks.First().Mutations.Count);
            Assert.AreEqual("1", workspace.Blocks.First().Mutations["else"]);

        }
    }
}
