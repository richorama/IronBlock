using System;
using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Math.Sqrt(9);"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Math.Sin(45 / (180 * Math.PI));"));
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
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Math.PI;"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("4 % 2 == 0;"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("3 % 2 == 1;"));
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
      Assert.ThrowsException<NotImplementedException>(() =>
      {
        new Parser()
                  .AddStandardBlocks()
                  .Parse(xml)
                  .Generate();
      });
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("7 % 1 == 0;"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("7.1 % 1 == 0;"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("7.1 > 0;"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("7.1 < 0;"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("9 % 3 == 0;"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Math.Round(3.1);"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Math.Ceiling(3.1);"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Math.Floor(3.1);"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Enumerable.Repeat(3, 5).ToList().Sum();"));
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
      Assert.ThrowsException<NotImplementedException>(() =>
      {
        new Parser()
                  .AddStandardBlocks()
                  .Parse(xml)
                  .Generate();
      });
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
      Assert.ThrowsException<NotImplementedException>(() =>
      {
        new Parser()
                  .AddStandardBlocks()
                  .Parse(xml)
                  .Generate();
      });
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Math.Min(Math.Max(110, 1), 100);"));
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
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("64 % 10;"));
    }

    /*


     */

  }
}
