using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class TextTests
    {
        [TestMethod]
        public void Test_Mutations()
        {
            const string xml = @"
<xml>
  <block type=""text_length"">
    <value name=""VALUE"">
      <shadow type=""text"">
        <field name=""TEXT"">abc</field>
      </shadow>
    </value>
  </block>
</xml>
";

            var output = new Parser()
              .AddStandardBlocks()
              .Parse(xml)
              .Evaluate();
            
            Assert.AreEqual(3, output);

        }
    }

}
