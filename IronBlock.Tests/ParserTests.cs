using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  [TestClass]
  public class ParserTests
  {
    [TestMethod]
    public void Test_Mutations()
    {
      const string xml = @"
<xml>
    <block type=""controls_if"">
        <mutation else=""1""/>
    </block>            
</xml>
";

      var parser = new Parser();
      parser.AddStandardBlocks();

      var workspace = parser.Parse(xml);

      Assert.AreEqual(1, workspace.Blocks.Count);
      Assert.AreEqual(1, workspace.Blocks.First().Mutations.Count);
      Assert.AreEqual("1", workspace.Blocks.First().Mutations.GetValue("else"));

    }


    [TestMethod]
    public void Test_Disabled()
    {
      const string xml = @"
<xml>
  <block type=""text_print"" disabled=""true"">
    <value name=""TEXT"">
      <block type=""text"">
        <field name=""TEXT"">abc</field>
      </block>
    </value>
  </block>
</xml>";

      var workspace = new Parser()
        .AddStandardBlocks()
        .Parse(xml);

      Assert.AreEqual(0, workspace.Blocks.Count);
    }


    [TestMethod]
    public void Test_Shadow()
    {
      const string xml = @"
<xml>
  <block type=""text_print"">
    <value name=""TEXT"">
      <shadow type=""text"">
        <field name=""TEXT"">abc</field>
      </shadow>
    </value>
  </block>
</xml>";

      new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("abc", TestExtensions.GetDebugText().First());
    }

    [TestMethod]
    public void Test_Args_Input()
    {
      const string xml = @"
<xml>
  <block type=""text_print"">
    <value name=""TEXT"">
      <block type=""variables_get"">
        <field name=""VAR"">message</field>
      </block>
    </value>
  </block>
</xml>";

      var args = new Dictionary<string, object>();
      args.Add("message", "Hello World");

      new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate(args);

      Assert.AreEqual("Hello World", TestExtensions.GetDebugText().First());
    }

    [TestMethod]
    public void Test_Args_Output()
    {
      const string xml = @"
<xml>
  <block type=""variables_set"" inline=""true"">
    <field name=""VAR"">output</field>
    <value name=""VALUE"">
      <block type=""text"">
        <field name=""TEXT"">Hello World</field>
      </block>
    </value>
  </block>
</xml>";

      var args = new Dictionary<string, object>();

      new Parser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate(args);

      Assert.AreEqual("Hello World", args["output"]);
    }

  }

}
