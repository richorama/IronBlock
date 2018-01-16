using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void Test_Procedure_No_Return()
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

            new Parser()
                .AddStandardBlocks()
                .AddDebugPrinter()
                .Parse(xml)
                .Evaluate();

            Assert.AreEqual("hello world,hello world,hello world", string.Join(",", TestExtensions.GetDebugText()));

        }


        [TestMethod]
        public void Test_Procedure_Return()
        {
            const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""procedures_defreturn"">
    <field name=""NAME"">do something</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <value name=""RETURN"">
      <block type=""text"" id=""4p1uAONhYe8wWJ};60Ff"">
        <field name=""TEXT"">hello world</field>
      </block>
    </value>
  </block>
  <block type=""procedures_callreturn"" id=""%qnT~o/4TK+nMt-tCrh6"" x=""238"" y=""113"">
    <mutation name=""do something""></mutation>
  </block>
</xml>
";

            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();

            Assert.AreEqual("hello world", output);

        }


        [TestMethod]
        public void Test_Procedure_If_Return()
        {
            const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""procedures_defreturn"">
    <field name=""NAME"">do something</field>
    <comment pinned=""false"">Describe this function...</comment>
    <statement name=""STACK"">
      <block type=""procedures_ifreturn"">
        <mutation value=""1""></mutation>
        <value name=""CONDITION"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
        <value name=""VALUE"">
          <block type=""text"">
            <field name=""TEXT"">hello world</field>
          </block>
        </value>
      </block>
    </statement>
    <value name=""RETURN"">
      <block type=""text"">
        <field name=""TEXT"">xxx</field>
      </block>
    </value>
  </block>
  <block type=""procedures_callreturn"">
    <mutation name=""do something""></mutation>
  </block>
</xml>
";

            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();

            Assert.AreEqual("hello world", output);

        }


    }
}

