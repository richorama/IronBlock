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


        [TestMethod]
        public void Test_Math_Number_Property_Even()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">EVEN</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">4</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(true, (bool) output);
        }

        [TestMethod]
        public void Test_Math_Number_Property_Odd()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">ODD</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">3</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(true, (bool) output);
        }


        [TestMethod]
        public void Test_Math_Number_Property_Prime()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">PRIME</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">29</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(true, (bool) output);
        }

        [TestMethod]
        public void Test_Math_Number_Property_Whole_True()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">WHOLE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(true, (bool) output);
        }

        [TestMethod]
        public void Test_Math_Number_Property_Whole_False()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">WHOLE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(false, (bool) output);
        }

        [TestMethod]
        public void Test_Math_Number_Property_Positive()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">POSITIVE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(true, (bool) output);
        }

        [TestMethod]
        public void Test_Math_Number_Property_Negative()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">NEGATIVE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(false, (bool) output);
        }

        [TestMethod]
        public void Test_Math_Number_Property_Divisible_By()
        {

            const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""true""></mutation>
    <field name=""PROPERTY"">DIVISIBLE_BY</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">9</field>
      </shadow>
    </value>
    <value name=""DIVISOR"">
      <block type=""math_number"">
        <field name=""NUM"">3</field>
      </block>
    </value>
  </block>
</xml>
";
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();
            
            Assert.AreEqual(true, (bool) output);
        }


    }
}
