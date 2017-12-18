using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class ColourTests
    {
        [TestMethod]
        public void Test_Colour_Picker()
        {
            const string xml = @"
<xml>
  <block type=""colour_picker"">
    <field name=""COLOUR"">#ff0000</field>
  </block>
</xml>
";

            var output = new Parser()
              .AddStandardBlocks()
              .Parse(xml)
              .Evaluate();
            
            Assert.AreEqual("#ff0000", output);

        }

        [TestMethod]
        public void Test_Colour_Random()
        {
            const string xml = @"
<xml>
  <block type=""colour_random""></block>
</xml>
";

            var program = new Parser()
              .AddStandardBlocks()
              .Parse(xml);

            var output = program.Evaluate() as string;
            
            Assert.AreEqual(7, output.Length);
            Assert.AreEqual('#', output[0]);

            // assure it's random by asking for another one
            var output2 = program.Evaluate() as string;
            Assert.AreNotEqual(output, output2);
        }


    }
}
