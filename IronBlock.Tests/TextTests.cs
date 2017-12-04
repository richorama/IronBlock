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
        public void Test_Text_Length()
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



        [TestMethod]
        public void Test_Text_IsEmpty()
        {
            const string xml = @"
<xml>
  <block type=""text_isEmpty"">
    <value name=""VALUE"">
      <shadow type=""text"">
        <field name=""TEXT""></field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = (bool) new Parser()
              .AddStandardBlocks()
              .Parse(xml)
              .Evaluate();
            
            Assert.IsTrue(output);
        }


    }

}
