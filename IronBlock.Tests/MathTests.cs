using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  [TestClass]
  public class MathTests
  {
    [TestMethod]
    public void Test_Math_Root()
    {

      const string xml = @"
<xml>
  <block type=""math_single"">
    <field name=""OP"">ROOT</field>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">9</field>
      </shadow>
    </value>
  </block>        
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(3.0, output);
    }

    [TestMethod]
    public void Test_Math_Sin()
    {

      const string xml = @"
<xml>
  <block type=""math_trig"">
    <field name=""OP"">SIN</field>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">45</field>
      </shadow>
    </value>
  </block>        
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(System.Math.Sin(System.Math.PI / 4), output);
    }

    [TestMethod]
    public void Test_Math_PI()
    {

      const string xml = @"
<xml>
  <block type=""math_constant"">
    <field name=""CONSTANT"">PI</field>
  </block>        
</xml>
";
      var workspace = new Parser()
          .AddStandardBlocks()
          .Parse(xml);

      var output = workspace.Evaluate();
      Assert.AreEqual(System.Math.PI, output);

      var csharp = workspace.Generate().NormalizeWhitespace().ToFullString();
      Assert.IsTrue(csharp.Contains("Math.PI"));
    }


    [TestMethod]
    public void Test_Math_Number_Property_Even()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">EVEN</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">4</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(true, (bool)output);
    }

    [TestMethod]
    public void Test_Math_Number_Property_Odd()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">ODD</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">3</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(true, (bool)output);
    }


    [TestMethod]
    public void Test_Math_Number_Property_Prime()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">PRIME</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">29</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(true, (bool)output);
    }

    [TestMethod]
    public void Test_Math_Number_Property_Whole_True()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">WHOLE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(true, (bool)output);
    }

    [TestMethod]
    public void Test_Math_Number_Property_Whole_False()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">WHOLE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(false, (bool)output);
    }

    [TestMethod]
    public void Test_Math_Number_Property_Positive()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">POSITIVE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(true, (bool)output);
    }

    [TestMethod]
    public void Test_Math_Number_Property_Negative()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""false""></mutation>
    <field name=""PROPERTY"">NEGATIVE</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">7.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(false, (bool)output);
    }

    [TestMethod]
    public void Test_Math_Number_Property_Divisible_By()
    {

      const string xml = @"
<xml>
  <block type=""math_number_property"">
    <mutation divisor_input=""true""></mutation>
    <field name=""PROPERTY"">DIVISIBLE_BY</field>
    <value name=""NUMBER_TO_CHECK"">
      <shadow type=""math_number"">
        <field name=""NUM"">9</field>
      </shadow>
    </value>
    <value name=""DIVISOR"">
      <block type=""math_number"">
        <field name=""NUM"">3</field>
      </block>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(true, (bool)output);
    }

    [TestMethod]
    public void Test_Math_Number_Round()
    {

      const string xml = @"
<xml>
  <block type=""math_round"">
    <field name=""OP"">ROUND</field>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">3.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(3.0, (double)output);
    }

    [TestMethod]
    public void Test_Math_Number_Round_Up()
    {

      const string xml = @"
<xml>
  <block type=""math_round"">
    <field name=""OP"">ROUNDUP</field>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">3.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(4.0, (double)output);
    }


    [TestMethod]
    public void Test_Math_Number_Round_Down()
    {

      const string xml = @"
<xml>
  <block type=""math_round"">
    <field name=""OP"">ROUNDDOWN</field>
    <value name=""NUM"">
      <shadow type=""math_number"">
        <field name=""NUM"">3.1</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(3.0, (double)output);
    }


    [TestMethod]
    public void Test_Math_On_List_Sum()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""math_on_list"">
    <mutation op=""SUM""></mutation>
    <field name=""OP"">SUM</field>
    <value name=""LIST"">
      <block type=""lists_repeat"">
        <value name=""ITEM"">
          <block type=""math_number"">
            <field name=""NUM"">3</field>
          </block>
        </value>
        <value name=""NUM"">
          <shadow type=""math_number"">
            <field name=""NUM"">5</field>
          </shadow>
        </value>
      </block>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(15, (double)output);
    }


    [TestMethod]
    public void Test_Math_On_List_Random()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""math_on_list"">
    <mutation op=""RANDOM""></mutation>
    <field name=""OP"">RANDOM</field>
    <value name=""LIST"">
      <block type=""lists_repeat"">
        <value name=""ITEM"">
          <block type=""math_number"">
            <field name=""NUM"">3</field>
          </block>
        </value>
        <value name=""NUM"">
          <shadow type=""math_number"">
            <field name=""NUM"">5</field>
          </shadow>
        </value>
      </block>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(3, (double)output);
    }


    [TestMethod]
    public void Test_Math_On_List_Mode()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""math_on_list"">
    <mutation op=""MODE""></mutation>
    <field name=""OP"">MODE</field>
    <value name=""LIST"">
      <block type=""lists_repeat"">
        <value name=""ITEM"">
          <block type=""math_number"">
            <field name=""NUM"">3</field>
          </block>
        </value>
        <value name=""NUM"">
          <shadow type=""math_number"">
            <field name=""NUM"">5</field>
          </shadow>
        </value>
      </block>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(3, (double)output);
    }

    [TestMethod]
    public void Test_Math_Constrain()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""math_constrain"">
    <value name=""VALUE"">
      <shadow type=""math_number"">
        <field name=""NUM"">110</field>
      </shadow>
    </value>
    <value name=""LOW"">
      <shadow type=""math_number"">
        <field name=""NUM"">1</field>
      </shadow>
    </value>
    <value name=""HIGH"">
      <shadow type=""math_number"">
        <field name=""NUM"">100</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual(100, (double)output);
    }

    [TestMethod]
    public void Test_Math_Moduluo()
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
          .Evaluate();

      Assert.AreEqual(4, (double)output);
    }

    [TestMethod]
    public void Test_Math_Random_Fraction()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables></variables>
   <block type=""math_random_float""></block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.IsTrue((double)output >= 0.0);
      Assert.IsTrue((double)output <= 1.0);
    }

    [TestMethod]
    public void Test_Math_Random_Int()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""math_random_int"">
    <value name=""FROM"">
      <shadow type=""math_number"">
        <field name=""NUM"">1</field>
      </shadow>
    </value>
    <value name=""TO"">
      <shadow type=""math_number"">
        <field name=""NUM"">100</field>
      </shadow>
    </value>
  </block>
</xml>
";
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Evaluate();

      Assert.IsTrue((double)output >= 1);
      Assert.IsTrue((double)output <= 100);
    }

    [TestMethod]
    public void Test_Math_Change()
    {

      const string xml = @"
<xml xmlns=""https://developers.google.com/blockly/xml"">
  <variables>
    <variable id=""ff`YJBi(D@smL[)Q:H}}"">foo</variable>
  </variables>
  <block type=""variables_set"" id=""a2aSu{9x7/D~9z3WGU(C"" x=""562"" y=""112"">
    <field name=""VAR"" id=""ff`YJBi(D@smL[)Q:H}}"">foo</field>
    <value name=""VALUE"">
      <block type=""math_number"" id=""(=?Qs,U~+c+hewlZejLb"">
        <field name=""NUM"">1</field>
      </block>
    </value>
    <next>
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
    </next>
  </block>
</xml>";

      var output = new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("2", TestExtensions.GetDebugText().First());

    }

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

      // Variable should be initialized to 0.0 and then incremented by 1
      Assert.AreEqual("1", TestExtensions.GetDebugText().First());
    }

    [TestMethod]
    public void Test_Math_Change_With_Null_Variable()
    {
      const string xml = @"
<xml xmlns=""https://developers.google.com/blockly/xml"">
  <variables>
    <variable id=""test123"">testVar</variable>
  </variables>
  <block type=""math_change"" id=""block123"">
    <field name=""VAR"" id=""test123"">testVar</field>
    <value name=""DELTA"">
      <block type=""math_number"" id=""num123"">
        <field name=""NUM"">5</field>
      </block>
    </value>
    <next>
      <block type=""text_print"" id=""print123"">
        <value name=""TEXT"">
          <block type=""variables_get"" id=""get123"">
            <field name=""VAR"" id=""test123"">testVar</field>
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

      // Variable should be initialized to 0.0 and then incremented by 5
      Assert.AreEqual("5", TestExtensions.GetDebugText().First());
    }



  }
}
