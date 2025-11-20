using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronBlock.Blocks;
using System.Linq;

namespace IronBlock.Tests
{
    [TestClass]
    public class UndeclaredVariableTest
    {
        [TestMethod]
        public void Test_Math_Change_With_Undeclared_Variable()
        {
            const string xml = @"
<xml xmlns=""https://developers.google.com/blockly/xml"">
  <variables>
    <variable id=""ff`YJBi(D@smL[)Q:H}}"">foo</variable>
  </variables>
  <block type=""math_change"" id=""uO~$6GN{K~{gOBd!r%vp"">
    <field name=""VAR"" id=""ff`YJBi(D@smL[)Q:H}}"">foo</field>
    <value name=""DELTA"">
      <shadow type=""math_number"" id=""S3n?jRy1.r1?+xGsN[ba"">
        <field name=""NUM"">1</field>
      </shadow>
    </value>
    <next>
      <block type=""text_print"" id=""ZzSeF~6sC{%k3b*c_2hm"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""x4}B?M9VFEOb;WmE^8ba"">
            <field name=""TEXT"">abc</field>
          </shadow>
          <block type=""variables_get"" id="")[}s1A^ZtI^hEMi5qjuw"">
            <field name=""VAR"" id=""ff`YJBi(D@smL[)Q:H}}"">foo</field>
          </block>
        </value>
      </block>
    </next>
  </block>
</xml>";

            var output = new Parser()
                .AddStandardBlocks()
                .AddDebugPrinter()
                .Parse(xml)
                .Evaluate();

            Assert.AreEqual("1", TestExtensions.GetDebugText().First());
        }
    }
}
