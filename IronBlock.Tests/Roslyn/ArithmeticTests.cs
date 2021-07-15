using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
{
  [TestClass]
  public class ArithmeticTests
  {
    [TestMethod]
    public void Test_Math_Operation_Add()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables></variables>
  <block id=""xYWLO1CBIc?5q]psGz;C"" type=""math_arithmetic"" y=""188"" x=""513"">
    <field name=""OP"">ADD</field>
    <value name=""A"">
      <shadow id=""N;S4N?}[0^h[2J`?IdT-"" type=""math_number"">
        <field name=""NUM"">2</field>
      </shadow>
    </value>
    <value name=""B"">
      <shadow id=""w/gZLNCQ3-#qYNhu1o!$"" type=""math_number"">
        <field name=""NUM"">4</field>
      </shadow>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("(2 + 4);"));
    }

    [TestMethod]
    public void Test_Math_Operation_Power()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables></variables>
  <block id=""xYWLO1CBIc?5q]psGz;C"" type=""math_arithmetic"" y=""188"" x=""513"">
    <field name=""OP"">POWER</field>
    <value name=""A"">
      <shadow id=""N;S4N?}[0^h[2J`?IdT-"" type=""math_number"">
        <field name=""NUM"">4</field>
      </shadow>
    </value>
    <value name=""B"">
      <shadow id=""w/gZLNCQ3-#qYNhu1o!$"" type=""math_number"">
        <field name=""NUM"">2</field>
      </shadow>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("(Math.Pow(4, 2));"));
    }

    [TestMethod]
    public void Test_Math_Operation_Add_With_Multiply()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables></variables>
  <block id=""v3etC{34uL,(%l=C)@}j"" type=""math_arithmetic"" y=""213"" x=""538"">
    <field name=""OP"">MULTIPLY</field>
    <value name=""A"">
      <block id=""xYWLO1CBIc?5q]psGz;C"" type=""math_arithmetic"">
        <field name=""OP"">ADD</field>
        <value name=""A"">
          <shadow id=""N;S4N?}[0^h[2J`?IdT-"" type=""math_number"">
            <field name=""NUM"">2</field>
          </shadow>
        </value>
        <value name=""B"">
          <shadow id=""w/gZLNCQ3-#qYNhu1o!$"" type=""math_number"">
            <field name=""NUM"">4</field>
          </shadow>
        </value>
      </block>
    </value>
    <value name=""B"">
      <shadow id=""s_9LSGUgmDr9f.oG-9:_"" type=""math_number"">
        <field name=""NUM"">5</field>
      </shadow>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("((2 + 4) * 5);"));
    }


    [TestMethod]
    public void Test_Math_Operation_Moduluo()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
<variables></variables>
<block type=""math_modulo"">
<value name=""DIVIDEND"">
  <shadow type=""math_number"">
    <field name=""NUM"">64</field>
  </shadow>
</value>
<value name=""DIVISOR"">
  <shadow type=""math_number"">
    <field name=""NUM"">10</field>
  </shadow>
</value>
</block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("64 % 10;"), code);
    }


  }
}
