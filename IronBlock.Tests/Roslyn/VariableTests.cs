using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
{
  [TestClass]
  public class VariableTests
  {
    [TestMethod]
    public void Test_Simple_Variable_Assignment()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""CyJvFZf3l6Qz{x[Xx5A^"" type="""">a</variable>
  </variables>
  <block id=""ip,}YZWkE^E;?RO+$}w-"" type=""variables_set"" x=""113"" y=""113"">
    <field id=""CyJvFZf3l6Qz{x[Xx5A^"" name=""VAR"" variabletype="""">a</field>
    <value name=""VALUE"">
      <block id=""ufU%[*OmU6UOh}}g`2{Q"" type=""math_number"">
        <field name=""NUM"">1</field>
      </block>
    </value>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("dynamic a;"));
      Assert.IsTrue(code.Contains("a = 1;"));
    }

    [TestMethod]
    public void Test_Complex_Variable_Assignment()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""CyJvFZf3l6Qz{x[Xx5A^"" type="""">a</variable>
    <variable id=""/(_We{Fatqdr6G[)kAa@"" type="""">b</variable>
  </variables>
  <block id=""ip,}YZWkE^E;?RO+$}w-"" type=""variables_set"" x=""113"" y=""113"">
    <field id=""CyJvFZf3l6Qz{x[Xx5A^"" name=""VAR"" variabletype="""">a</field>
    <value name=""VALUE"">
      <block id=""ufU%[*OmU6UOh}}g`2{Q"" type=""math_number"">
        <field name=""NUM"">5</field>
      </block>
    </value>
    <next>
      <block id=""=h.[*kNgW)~s%}yAXEy;"" type=""variables_set"">
        <field id=""/(_We{Fatqdr6G[)kAa@"" name=""VAR"" variabletype="""">b</field>
        <value name=""VALUE"">
          <block id=""2!U:PG=Kv/nZ6cLuDH0i"" type=""math_number"">
            <field name=""NUM"">8</field>
          </block>
        </value>
        <next>
          <block id=""Z.^8c:sSI7S9K;9fA~k`"" type=""variables_set"">
            <field id=""/(_We{Fatqdr6G[)kAa@"" name=""VAR"" variabletype="""">b</field>
            <value name=""VALUE"">
              <block id=""WA?c7!*910hd*)m9v|pW"" type=""math_arithmetic"">
                <field name=""OP"">ADD</field>
                <value name=""A"">
                  <block id=""N*XiY,_[6TMlm,3Z1$ur"" type=""variables_get"">
                    <field id=""CyJvFZf3l6Qz{x[Xx5A^"" name=""VAR"" variabletype="""">a</field>
                  </block>
                </value>
                <value name=""B"">
                  <block id=""#L_@3{06y1PLH{}OdiU_"" type=""variables_get"">
                    <field id=""/(_We{Fatqdr6G[)kAa@"" name=""VAR"" variabletype="""">b</field>
                  </block>
                </value>
              </block>
            </value>
          </block>
        </next>
      </block>
    </next>
  </block>
</xml>";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("dynamic a;"));
      Assert.IsTrue(code.Contains("dynamic b;"));
      Assert.IsTrue(code.Contains("a = 5;"));
      Assert.IsTrue(code.Contains("b = 8;"));
      Assert.IsTrue(code.Contains("b = (a + b);"));

    }
  }
}
