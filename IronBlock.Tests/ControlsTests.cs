using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class ControlsTests
    {
        [TestMethod]
        public void Test_Controls_If()
        {

            const string xml = @"
<xml>
  <block type=""controls_if"" >
    <value name=""IF0"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"">
        <value name=""TEXT"">
          <shadow type=""text"">
            <field name=""TEXT"">success</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>       
</xml>
";
            var parser = new Parser().AddStandardBlocks();
            var printer = parser.AddDebugPrinter();
            parser.Parse(xml).Evaluate();
            
            Assert.AreEqual("success", printer.Text.First());
        }


                [TestMethod]
        public void Test_Controls_WhileUntil()
        {

            const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable type="""">x</variable>
  </variables>
  <block type=""variables_set"">
    <field name=""VAR"" variabletype="""">x</field>
    <value name=""VALUE"">
      <block type=""math_number"">
        <field name=""NUM"">0</field>
      </block>
    </value>
    <next>
      <block type=""controls_whileUntil"">
        <field name=""MODE"">WHILE</field>
        <value name=""BOOL"">
          <block type=""logic_compare"">
            <field name=""OP"">EQ</field>
            <value name=""A"">
              <block type=""variables_get"">
                <field name=""VAR"" variabletype="""">x</field>
              </block>
            </value>
            <value name=""B"">
              <block type=""math_number"">
                <field name=""NUM"">0</field>
              </block>
            </value>
          </block>
        </value>
        <statement name=""DO"">
          <block type=""text_print"">
            <value name=""TEXT"">
              <shadow type=""text"">
                <field name=""TEXT"">abc</field>
              </shadow>
              <block type=""variables_get"">
                <field name=""VAR"" variabletype="""">x</field>
              </block>
            </value>
            <next>
              <block type=""variables_set"">
                <field name=""VAR"" variabletype="""">x</field>
                <value name=""VALUE"">
                  <block type=""math_number"">
                    <field name=""NUM"">1</field>
                  </block>
                </value>
              </block>
            </next>
          </block>
        </statement>
      </block>
    </next>
  </block>
</xml>
";
            var parser = new Parser().AddStandardBlocks();
            var printer = parser.AddDebugPrinter();
            parser.Parse(xml).Evaluate();
            
            Assert.AreEqual("0", string.Join(",",printer.Text));
        }


    }
}
