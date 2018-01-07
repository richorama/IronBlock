using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void Test_Text_Length()
        {
            const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable type="""">x</variable>
  </variables>
  <block type=""procedures_defnoreturn"">
    <mutation>
      <arg name=""x""></arg>
    </mutation>
    <field name=""NAME"">do something</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <statement name=""STACK"">
      <block type=""text_print"">
        <value name=""TEXT"">
          <shadow type=""text"">
            <field name=""TEXT"">abc</field>
          </shadow>
          <block type=""variables_get"" >
            <field name=""VAR"" variabletype="""">x</field>
          </block>
        </value>
      </block>
    </statement>
  </block>
  <block type=""controls_repeat_ext"">
    <value name=""TIMES"">
      <shadow type=""math_number"">
        <field name=""NUM"">3</field>
      </shadow>
    </value>
    <statement name=""DO"">
      <block type=""procedures_callnoreturn"">
        <mutation name=""do something"">
          <arg name=""x""></arg>
        </mutation>
        <value name=""ARG0"">
          <block type=""text"">
            <field name=""TEXT"">hello world</field>
          </block>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

            var parser = new Parser();

            parser.AddStandardBlocks();
            var printer = parser.AddDebugPrinter();
            parser.Parse(xml).Evaluate();

            Assert.AreEqual("hello world,hello world,hello world", string.Join(",", printer.Text));

        }




    }
}

