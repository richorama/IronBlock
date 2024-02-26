using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  [TestClass]
  public class ListsTests
  {

    [TestMethod]
    public void Test_List_Create_With()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""lists_create_with"">
    <mutation items=""3""></mutation>
    <value name=""ADD0"">
      <block type=""text"">
        <field name=""TEXT"">x</field>
      </block>
    </value>
    <value name=""ADD1"">
      <block type=""text"">
        <field name=""TEXT"">y</field>
      </block>
    </value>
    <value name=""ADD2"">
      <block type=""text"">
        <field name=""TEXT"">z</field>
      </block>
    </value>
  </block>
</xml>            
";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("x,y,z", string.Join(",", (output as IEnumerable<object>).Select(x => x.ToString())));
    }



    [TestMethod]
    public void Test_List_Split()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""lists_split"">
    <mutation mode=""SPLIT""></mutation>
    <field name=""MODE"">SPLIT</field>
    <value name=""INPUT"">
      <block type=""text"">
        <field name=""TEXT"">x,y,z</field>
      </block>
    </value>
    <value name=""DELIM"">
      <shadow type=""text"">
        <field name=""TEXT"">,</field>
      </shadow>
    </value>
  </block>
</xml>
";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("x,y,z", string.Join(",", output as IEnumerable<object>));

    }

    [TestMethod]
    public void Test_Lists_Join()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""lists_split"">
    <mutation mode=""JOIN""></mutation>
    <field name=""MODE"">JOIN</field>
    <value name=""INPUT"">
      <block type=""lists_create_with"">
        <mutation items=""3""></mutation>
        <value name=""ADD0"">
          <block type=""text"">
            <field name=""TEXT"">x</field>
          </block>
        </value>
        <value name=""ADD1"">
          <block type=""text"">
            <field name=""TEXT"">y</field>
          </block>
        </value>
        <value name=""ADD2"">
          <block type=""text"">
            <field name=""TEXT"">z</field>
          </block>
        </value>
      </block>
    </value>
    <value name=""DELIM"">
      <shadow type=""text"">
        <field name=""TEXT"">,</field>
      </shadow>
    </value>
  </block>
</xml>            
            ";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("x,y,z", output);

    }



    [TestMethod]
    public void Test_Lists_Length()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""lists_length"">
    <value name=""VALUE"">
      <block type=""lists_split"">
        <mutation mode=""SPLIT""></mutation>
        <field name=""MODE"">SPLIT</field>
        <value name=""INPUT"">
          <block type=""text"">
            <field name=""TEXT"">a,b,c</field>
          </block>
        </value>
        <value name=""DELIM"">
          <shadow type=""text"">
            <field name=""TEXT"">,</field>
          </shadow>
        </value>
      </block>
    </value>
  </block>
</xml>
";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual(3, (double)output);

    }


    [TestMethod]
    public void Test_Lists_Repeat()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""lists_repeat"">
    <value name=""ITEM"">
      <block type=""text"">
        <field name=""TEXT"">hello</field>
      </block>
    </value>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">3</field>
      </shadow>
    </value>
  </block>
</xml>
";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("hello,hello,hello", string.Join(",", (output as IEnumerable<object>).Select(x => x.ToString())));

    }


    [TestMethod]
    public void Test_Lists_IsEmpty()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""lists_isEmpty"">
    <value name=""VALUE"">
      <block type=""lists_create_with"">
        <mutation items=""0""></mutation>
      </block>
    </value>
  </block>
</xml>
";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.IsTrue((bool)output);

    }

    [TestMethod]
    public void Test_Lists_IndexOf()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables></variables>
  <block type=""lists_indexOf"">
    <field name=""END"">FIRST</field>
    <value name=""VALUE"">
      <block type=""lists_split"">
        <mutation mode=""SPLIT""></mutation>
        <field name=""MODE"">SPLIT</field>
        <value name=""INPUT"">
          <block type=""text"">
            <field name=""TEXT"">foo,bar,baz</field>
          </block>
        </value>
        <value name=""DELIM"">
          <shadow type=""text"">
            <field name=""TEXT"">,</field>
          </shadow>
        </value>
      </block>
    </value>
    <value name=""FIND"">
      <block type=""text"">
        <field name=""TEXT"">bar</field>
      </block>
    </value>
  </block>
</xml>
";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual(2, (int)output);

    }

    [TestMethod]
    public void Test_Lists_GetIndex()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""lists_getIndex"">
    <mutation statement=""false"" at=""true""></mutation>
    <field name=""MODE"">GET</field>
    <field name=""WHERE"">FROM_START</field>
    <value name=""VALUE"">
      <block type=""lists_split"">
        <mutation mode=""SPLIT""></mutation>
        <field name=""MODE"">SPLIT</field>
        <value name=""INPUT"">
          <block type=""text"">
            <field name=""TEXT"">foo,bar,baz</field>
          </block>
        </value>
        <value name=""DELIM"">
          <shadow type=""text"">
            <field name=""TEXT"">,</field>
          </shadow>
        </value>
      </block>
    </value>
    <value name=""AT"">
      <block type=""math_number"">
        <field name=""NUM"">2</field>
      </block>
    </value>
  </block>
</xml>
";

      var output = new XmlParser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("bar", (string)output);

    }

  }
}
