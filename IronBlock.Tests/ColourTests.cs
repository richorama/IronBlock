using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  [TestClass]
  public class ColourTests
  {
    [TestMethod]
    public void Test_Colour_Picker()
    {
      const string xml = @"
<xml>
  <block type=""colour_picker"">
    <field name=""COLOUR"">#ff0000</field>
  </block>
</xml>
";

      var output = new Parser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("#ff0000", output);

    }

    [TestMethod]
    public void Test_Colour_Random()
    {
      const string xml = @"
<xml>
  <block type=""colour_random""></block>
</xml>
";

      var program = new Parser()
        .AddStandardBlocks()
        .Parse(xml);

      var output = program.Evaluate() as string;

      Assert.AreEqual(7, output!.Length);
      Assert.AreEqual('#', output[0]);

      // assure it's random by asking for another one
      var output2 = program.Evaluate() as string;
      Assert.AreNotEqual(output, output2);
    }


    [TestMethod]
    public void Test_Colour_Rgb()
    {
      const string xml = @"
<xml>
 <block type=""colour_rgb"" id=""JM:^k(U=gB8g^%oXS3}#"" x=""188"" y=""88"">
    <value name=""RED"">
      <shadow type=""math_number"" id="":R|JG4bNKO%{Imqb80Ra"">
        <field name=""NUM"">255</field>
      </shadow>
    </value>
    <value name=""GREEN"">
      <shadow type=""math_number"" id=""u)A6y/5^OS?,@4_[qH#f"">
        <field name=""NUM"">0</field>
      </shadow>
    </value>
    <value name=""BLUE"">
      <shadow type=""math_number"" id=""QygYvsLu_]am-bn9M_S-"">
        <field name=""NUM"">1</field>
      </shadow>
    </value>
  </block>
</xml>
";

      var colour = new Parser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate() as string;

      Assert.AreEqual("#ff0001", colour);

    }


    [TestMethod]
    public void Test_Colour_Blend()
    {
      const string xml = @"
<xml>
  <block type=""colour_blend"">
    <value name=""COLOUR1"">
      <shadow type=""colour_picker"">
        <field name=""COLOUR"">#ff0000</field>
      </shadow>
    </value>
    <value name=""COLOUR2"">
      <shadow type=""colour_picker"">
        <field name=""COLOUR"">#3333ff</field>
      </shadow>
    </value>
    <value name=""RATIO"">
      <shadow type=""math_number"">
        <field name=""NUM"">0.2</field>
      </shadow>
    </value>
  </block>
</xml>
";

      var colour = new Parser()
        .AddStandardBlocks()
        .Parse(xml)
        .Evaluate() as string;

      Assert.AreEqual("#d60a33", colour);

    }


  }
}
