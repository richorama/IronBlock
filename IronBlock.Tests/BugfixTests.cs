using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class BugfixTests
    {
        [TestMethod]
        public void Issue_17()
        {
            const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""controls_if"" id=""`Du+(_vc^5di^SX-r)Gy"" x=""-563"" y=""-187"">
    <value name=""IF0"">
      <block type=""logic_compare"" id=""IzaPI5pFR:fkFMJ@)E]9"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block type=""math_number"" id=""?M*ou@g~A#r=O3ic;$(5"">
            <field name=""NUM"">1</field>
          </block>
        </value>
        <value name=""B"">
          <block type=""text_indexOf"" id=""G`rDOxciV.H@2e(^)d$r"">
            <field name=""END"">FIRST</field>
            <value name=""VALUE"">
              <block type=""text"" id=""ofLQvK5G,]B7,_IWMu1W"">
                <field name=""TEXT"">foo bar baz</field>
              </block>
            </value>
            <value name=""FIND"">
              <shadow type=""text"" id=""c0^@7@Cn!{_2GLo/3ki`"">
                <field name=""TEXT"">foo</field>
              </shadow>
            </value>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"" id=""vhk7~#CLW_:TTeXy#.34"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""x?1zGi1mkq)$XIq7*t_-"">
            <field name=""TEXT"">it worked</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

            var output = new Parser()
              .AddStandardBlocks()
              .AddDebugPrinter()
              .Parse(xml)
              .Evaluate();

            Assert.AreEqual("it worked", TestExtensions.GetDebugText().First());

        }



[TestMethod]
        public void Issue_40()
        {
            const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""controls_if"" id=""`Du+(_vc^5di^SX-r)Gy"" x=""-563"" y=""-187"">
    <value name=""IF0"">
      <block type=""logic_compare"" id=""IzaPI5pFR:fkFMJ@)E]9"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
        <value name=""B"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"" id=""vhk7~#CLW_:TTeXy#.34"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""x?1zGi1mkq)$XIq7*t_-"">
            <field name=""TEXT"">it worked</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

            var output = new Parser()
              .AddStandardBlocks()
              .AddDebugPrinter()
              .Parse(xml)
              .Evaluate();

            Assert.AreEqual("it worked", TestExtensions.GetDebugText().First());

        }
    }
}




