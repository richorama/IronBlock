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


        [TestMethod]
        public void Test_Text_Trim()
        {
            const string xml = @"
<xml>
  <block type=""text_trim"">
    <field name=""MODE"">BOTH</field>
    <value name=""TEXT"">
      <shadow type=""text"">
        <field name=""TEXT""> ab c </field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
              .AddStandardBlocks()
              .Parse(xml)
              .Evaluate()
              .ToString();
            
            Assert.AreEqual("ab c", output);
        }

        [TestMethod]
        public void Test_Text_ToCase()
        {
            const string xml = @"
<xml>
  <block type=""text_changeCase"">
    <field name=""CASE"">TITLECASE</field>
    <value name=""TEXT"">
      <shadow type=""text"">
        <field name=""TEXT"">hello world</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
              .AddStandardBlocks()
              .Parse(xml)
              .Evaluate()
              .ToString();
            
            Assert.AreEqual("Hello World", output);
        }



        [TestMethod]
        public void Test_Text_Append()
        {
            const string xml = @"
<xml>
 <variables>
    <variable type="""">x</variable>
  </variables>
  <block type=""variables_set"">
    <field name=""VAR"">x</field>
    <value name=""VALUE"">
      <block type=""text"">
        <field name=""TEXT"">foo</field>
      </block>
    </value>
    <next>
      <block type=""text_append"">
        <field name=""VAR"">x</field>
        <value name=""TEXT"">
          <shadow type=""text"">
            <field name=""TEXT"">bar</field>
          </shadow>
        </value>
        <next>
          <block type=""text_print"">
            <value name=""TEXT"">
              <shadow type=""text"">
                <field name=""TEXT"">abc</field>
              </shadow>
              <block type=""variables_get"">
                <field name=""VAR"">x</field>
              </block>
            </value>
          </block>
        </next>
      </block>
    </next>
  </block>
</xml>
";
            new Parser()
                .AddStandardBlocks()
                .AddDebugPrinter()
                .Parse(xml)
                .Evaluate();

            Assert.AreEqual("foobar", TestExtensions.GetDebugText().First());
        }

        [TestMethod]
        public void Test_Text_Join()
        {
            const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable>x</variable>
  </variables>
  <block type=""variables_set"">
    <field name=""VAR"">x</field>
    <value name=""VALUE"">
      <block type=""text_join"">
        <mutation items=""3""></mutation>
        <value name=""ADD0"">
          <block type=""text"">
            <field name=""TEXT"">foo</field>
          </block>
        </value>
        <value name=""ADD1"">
          <block type=""text"">
            <field name=""TEXT"">bar</field>
          </block>
        </value>
      </block>
    </value>
    <next>
      <block type=""text_print"">
        <value name=""TEXT"">
          <shadow type=""text"">
            <field name=""TEXT"">abc</field>
          </shadow>
          <block type=""variables_get"">
            <field name=""VAR"">x</field>
          </block>
        </value>
      </block>
    </next>
  </block>
</xml>
";
            new Parser()
                .AddStandardBlocks()
                .AddDebugPrinter()
                .Parse(xml)
                .Evaluate();

            Assert.AreEqual("foobar", TestExtensions.GetDebugText().First());
        }


        [TestMethod]
        public void Test_IndexOf()
        {
            const string xml = @"
<xml>
  <block type=""text_indexOf"">
    <field name=""END"">FIRST</field>
    <value name=""VALUE"">
      <block type=""text"">
        <field name=""TEXT"">foo bar baz</field>
      </block>
    </value>
    <value name=""FIND"">
      <shadow type=""text"">
        <field name=""TEXT"">bar</field>
      </shadow>
    </value>
  </block>
</xml>
";
            var output = new Parser()
              .AddStandardBlocks()
              .Parse(xml)
              .Evaluate();
            
            Assert.AreEqual(5, output);
        }

    }
}
