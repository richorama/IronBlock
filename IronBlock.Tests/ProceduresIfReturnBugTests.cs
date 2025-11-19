using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  /// <summary>
  /// Tests demonstrating the bug with procedures_ifreturn when nested inside other control structures.
  /// 
  /// The issue: procedures_ifreturn should immediately exit the procedure when the condition is true,
  /// but currently it only returns from the immediate block context. When nested inside an if statement
  /// or loop, the return value is lost and execution continues.
  /// 
  /// The fix requires:
  /// 1. Adding a new EscapeMode.Return enum value
  /// 2. Modifying ProceduresIfReturn.Evaluate() to set context.EscapeMode = EscapeMode.Return
  /// 3. Storing the return value in the context (need new field like context.ReturnValue)
  /// 4. Modifying all control flow blocks (ControlsIf, loops, etc.) to check for EscapeMode.Return
  ///    and immediately return without processing further blocks
  /// 5. Modifying ProceduresCallReturn.Evaluate() to check for EscapeMode.Return and return the stored value
  /// </summary>
  [TestClass]
  public class ProceduresIfReturnBugTests
  {
    [TestMethod]
    public void Test_ProcedureIfReturn_InsideIf_ShouldReturnFromProcedure()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""procedures_defreturn"">
    <field name=""NAME"">test_function</field>
    <statement name=""STACK"">
      <block type=""controls_if"">
        <value name=""IF0"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
        <statement name=""DO0"">
          <block type=""procedures_ifreturn"">
            <mutation value=""1""></mutation>
            <value name=""CONDITION"">
              <block type=""logic_boolean"">
                <field name=""BOOL"">TRUE</field>
              </block>
            </value>
            <value name=""VALUE"">
              <block type=""text"">
                <field name=""TEXT"">early return</field>
              </block>
            </value>
          </block>
        </statement>
      </block>
    </statement>
    <value name=""RETURN"">
      <block type=""text"">
        <field name=""TEXT"">should not reach here</field>
      </block>
    </value>
  </block>
  <block type=""procedures_callreturn"">
    <mutation name=""test_function""></mutation>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      // Expected: "early return"
      // Actual: "should not reach here" (the return value is lost and execution continues)
      Assert.AreEqual("early return", output);
    }

    [TestMethod]
    public void Test_ProcedureIfReturn_InsideLoop_ShouldReturnFromProcedure()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""procedures_defreturn"">
    <field name=""NAME"">find_number</field>
    <statement name=""STACK"">
      <block type=""controls_repeat_ext"">
        <value name=""TIMES"">
          <block type=""math_number"">
            <field name=""NUM"">5</field>
          </block>
        </value>
        <statement name=""DO"">
          <block type=""procedures_ifreturn"">
            <mutation value=""1""></mutation>
            <value name=""CONDITION"">
              <block type=""logic_boolean"">
                <field name=""BOOL"">TRUE</field>
              </block>
            </value>
            <value name=""VALUE"">
              <block type=""text"">
                <field name=""TEXT"">found it</field>
              </block>
            </value>
          </block>
        </statement>
      </block>
    </statement>
    <value name=""RETURN"">
      <block type=""text"">
        <field name=""TEXT"">not found</field>
      </block>
    </value>
  </block>
  <block type=""procedures_callreturn"">
    <mutation name=""find_number""></mutation>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      // Expected: "found it" (should return on first iteration)
      // Actual: "not found" (the return value is lost, loop continues, then final return is executed)
      Assert.AreEqual("found it", output);
    }

    [TestMethod]
    public void Test_ProcedureIfReturn_TopLevel_WorksCorrectly()
    {
      // This test passes - it's only when nested that the bug occurs
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""procedures_defreturn"">
    <field name=""NAME"">do_something</field>
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
            <field name=""TEXT"">early return</field>
          </block>
        </value>
      </block>
    </statement>
    <value name=""RETURN"">
      <block type=""text"">
        <field name=""TEXT"">default return</field>
      </block>
    </value>
  </block>
  <block type=""procedures_callreturn"">
    <mutation name=""do_something""></mutation>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      // This works correctly because it's at the top level of the procedure
      Assert.AreEqual("early return", output);
    }
  }
}
