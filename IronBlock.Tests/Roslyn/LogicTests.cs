using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
{
  [TestClass]
  public class LogicTests
  {
    [TestMethod]
    public void Test_Logic_Boolean()
    {

      const string xml = @"
<xml>
    <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
    </block>           
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("true;"));
    }

    [TestMethod]
    public void Test_Logic_Compare_Equals()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""logic_compare"">
    <field name=""OP"">EQ</field>
    <value name=""A"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">FALSE</field>
      </block>
    </value>
    <value name=""B"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("(false == true);"));
    }


    [TestMethod]
    public void Test_Logic_Operation_Or()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""logic_operation"">
    <field name=""OP"">OR</field>
    <value name=""A"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">FALSE</field>
      </block>
    </value>
    <value name=""B"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("(false || true);"));
    }

    [TestMethod]
    public void Test_Logic_Operation_And()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""logic_operation"">
    <field name=""OP"">AND</field>
    <value name=""A"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">FALSE</field>
      </block>
    </value>
    <value name=""B"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("(false && true);"));
    }

    [TestMethod]
    public void Test_Logic_Negate()
    {

      const string xml = @"
<xml>
  <block type=""logic_negate"">
    <value name=""BOOL"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("!true;"));
    }

    [TestMethod]
    public void Test_Logic_Null()
    {

      const string xml = @"
<xml>
  <block type=""logic_null""></block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("null;"));
    }

    [TestMethod]
    public void Test_Logic_Ternary()
    {

      const string xml = @"
<xml>
  <block type=""logic_ternary"">
    <value name=""IF"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
    <value name=""THEN"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">FALSE</field>
      </block>
    </value>
    <value name=""ELSE"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("true ? false : true;"));
    }
  }
}
