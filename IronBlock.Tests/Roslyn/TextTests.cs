using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
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
        .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("\"abc\".Length;"));
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
      var output = new Parser()
        .AddStandardBlocks()
        .Parse(xml)
        .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("string.IsNullOrEmpty(\"\");"));
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
        .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("\" ab c \".Trim();"));
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
        .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("CultureInfo.InvariantCulture.TextInfo.ToTitleCase(\"hello world\");"));
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
      var output = new Parser()
        .AddStandardBlocks()
        .Parse(xml)
        .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic x; x = \"foo\"; x += \"bar\"; Console.WriteLine(x);"));
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
      var output = new Parser()
        .AddStandardBlocks()
        .Parse(xml)
        .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic x; x = string.Concat(\"foo\", \"bar\"); Console.WriteLine(x);"));
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
        .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("\"foo bar baz\".IndexOf(\"bar\");"));
    }


    [TestMethod]
    public void Test_Comment()
    {
      const string xml = @"
<xml>
  <block type=""text_print"">
    <comment>A test comment</comment>
    <field name=""MODE"">BOTH</field>
    <value name=""TEXT"">
      <shadow type=""text"">
        <field name=""TEXT"">Hello World</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var blocks = new Parser()
        .AddStandardBlocks()
        .Parse(xml);

      Assert.AreEqual(1, blocks.Blocks.First().Comments.Count);
      Assert.AreEqual("A test comment", blocks.Blocks.First().Comments.First().Value);
      var code = blocks.Generate().NormalizeWhitespace().ToFullString();

      Console.WriteLine(code);
      Assert.IsTrue(code.Contains("/* A test comment */"));

    }


  }
}
