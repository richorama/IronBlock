using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void Test_Math_Root()
        {

            const string xml = @"
<xml>
  <block type=""math_single"">
    <field name=""OP"">ROOT</field>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">9</field>
      </shadow>
    </value>
  </block>        
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(3.0, output);
        }

        [TestMethod]
        public void Test_Math_Sin()
        {

            const string xml = @"
<xml>
  <block type=""math_trig"">
    <field name=""OP"">SIN</field>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">45</field>
      </shadow>
    </value>
  </block>        
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(System.Math.Sin(System.Math.PI / 4), output);
        }

        [TestMethod]
        public void Test_Math_PI()
        {

            const string xml = @"
<xml>
  <block type=""math_constant"">
    <field name=""CONSTANT"">PI</field>
  </block>        
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(System.Math.PI, output);
        }


    }
}
