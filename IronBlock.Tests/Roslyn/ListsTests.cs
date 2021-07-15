using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
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
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("new List<dynamic>{\"x\", \"y\", \"z\"};"));
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
      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("\"x,y,z\".ToString(CultureInfo.InvariantCulture).Split(\",\");"));
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

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("string.Join(\",\", new List<dynamic>{\"x\", \"y\", \"z\"});"));
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

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("\"a,b,c\".ToString(CultureInfo.InvariantCulture).Split(\",\").Length;"));
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

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("Enumerable.Repeat(\"hello\", 3).ToList();"));
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

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace().ToFullString();
      Assert.IsTrue(code.Contains("new List<dynamic>{}.Any();"));
    }

    [TestMethod]
    public void Test_Lists_GetIndex_FromStart()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable id=""9b-j!_6?v^-pfbSI7(VI"" type="""">item</variable>
  </variables>
  <block id=""U}y~nesz6^ux9T0#rU}V"" type=""variables_set"" y=""238"" x=""-1212"">
    <field id=""9b-j!_6?v^-pfbSI7(VI"" name=""VAR"" variabletype="""">item</field>
    <value name=""VALUE"">
      <block id=""($|1[d$OJJAHYmHjR5-m"" type=""lists_getIndex"">
        <mutation at=""true"" statement=""false""></mutation>
        <field name=""MODE"">GET</field>
        <field name=""WHERE"">FROM_START</field>
        <value name=""VALUE"">
          <block id=""2u*W2+J|Vf8ILb=Xi7G+"" type=""lists_repeat"">
            <value name=""ITEM"">
              <block id=""%o:wizv5^Ts]ARR-EUb|"" type=""math_number"">
                <field name=""NUM"">10</field>
              </block>
            </value>
            <value name=""NUM"">
              <shadow id=""iZd0[QYzS}trPhQyc5fo"" type=""math_number"">
                <field name=""NUM"">5</field>
              </shadow>
            </value>
          </block>
        </value>
        <value name=""AT"">
          <block id="";;TqU}v2{_ABZ|m*m@Q0"" type=""math_number"">
            <field name=""NUM"">2</field>
          </block>
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

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic item; item = Enumerable.Repeat(10, 5).ToList()[2 - 1];"));
    }

    [TestMethod]
    public void Test_Lists_GetIndex_FromEnd()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable id=""9b-j!_6?v^-pfbSI7(VI"" type="""">item</variable>
  </variables>
  <block id=""U}y~nesz6^ux9T0#rU}V"" type=""variables_set"" y=""238"" x=""-1212"">
    <field id=""9b-j!_6?v^-pfbSI7(VI"" name=""VAR"" variabletype="""">item</field>
    <value name=""VALUE"">
      <block id=""($|1[d$OJJAHYmHjR5-m"" type=""lists_getIndex"">
        <mutation at=""true"" statement=""false""></mutation>
        <field name=""MODE"">GET</field>
        <field name=""WHERE"">FROM_END</field>
        <value name=""VALUE"">
          <block id=""~.6@C0+t)?KcBZ4nrH(/"" type=""lists_create_with"">
            <mutation items=""0""></mutation>
          </block>
        </value>
        <value name=""AT"">
          <block id="";;TqU}v2{_ABZ|m*m@Q0"" type=""math_number"">
            <field name=""NUM"">2</field>
          </block>
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

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic item; item = new List<dynamic>{}.TakeLast(2).First();"));
    }

    [TestMethod]
    public void Test_Lists_GetIndex_First()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable id=""9b-j!_6?v^-pfbSI7(VI"" type="""">item</variable>
  </variables>
  <block id=""U}y~nesz6^ux9T0#rU}V"" type=""variables_set"" y=""238"" x=""-1212"">
    <field id=""9b-j!_6?v^-pfbSI7(VI"" name=""VAR"" variabletype="""">item</field>
    <value name=""VALUE"">
      <block id=""($|1[d$OJJAHYmHjR5-m"" type=""lists_getIndex"">
        <mutation at=""true"" statement=""false""></mutation>
        <field name=""MODE"">GET</field>
        <field name=""WHERE"">FIRST</field>
        <value name=""VALUE"">
          <block id=""~.6@C0+t)?KcBZ4nrH(/"" type=""lists_create_with"">
            <mutation items=""0""></mutation>
          </block>
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

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic item; item = new List<dynamic>{}.First();"));
    }

    [TestMethod]
    public void Test_Lists_GetIndex_Last()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable id=""9b-j!_6?v^-pfbSI7(VI"" type="""">item</variable>
  </variables>
  <block id=""U}y~nesz6^ux9T0#rU}V"" type=""variables_set"" y=""238"" x=""-1212"">
    <field id=""9b-j!_6?v^-pfbSI7(VI"" name=""VAR"" variabletype="""">item</field>
    <value name=""VALUE"">
      <block id=""($|1[d$OJJAHYmHjR5-m"" type=""lists_getIndex"">
        <mutation at=""true"" statement=""false""></mutation>
        <field name=""MODE"">GET</field>
        <field name=""WHERE"">LAST</field>
        <value name=""VALUE"">
          <block id=""~.6@C0+t)?KcBZ4nrH(/"" type=""lists_create_with"">
            <mutation items=""0""></mutation>
          </block>
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

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic item; item = new List<dynamic>{}.Last();"));
    }

    [TestMethod]
    public void Test_Lists_SetIndex_FromStart()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""X.f5Y~hcMogYGWy/jV#E"" type="""">list</variable>
  </variables>
  <block id=""EI781:1nxTF=088/HJJ5"" type=""variables_set"" x=""263"" y=""288"">
    <field id=""X.f5Y~hcMogYGWy/jV#E"" name=""VAR"" variabletype="""">list</field>
    <value name=""VALUE"">
      <block id="";b/7T{A~*-N,M5!oiGne"" type=""lists_create_with"">
        <mutation items=""2""></mutation>
        <value name=""ADD0"">
          <block id=""2=1U#!u^Z$!nch108HXz"" type=""math_number"">
            <field name=""NUM"">10</field>
          </block>
        </value>
        <value name=""ADD1"">
          <block id=""adN@6Uy:f.QZ%7.!I8!Z"" type=""math_number"">
            <field name=""NUM"">20</field>
          </block>
        </value>
      </block>
    </value>
    <next>
      <block id=""M~!--)5@O{KOhd.E31gZ"" type=""lists_setIndex"">
        <mutation at=""true""></mutation>
        <field name=""MODE"">SET</field>
        <field name=""WHERE"">FROM_START</field>
        <value name=""LIST"">
          <block id=""m,G/pT4!CuERnWGImwgH"" type=""variables_get"">
            <field id=""X.f5Y~hcMogYGWy/jV#E"" name=""VAR"" variabletype="""">list</field>
          </block>
        </value>
        <value name=""AT"">
          <block id=""h}rIRz1]]DfYRZR)=6Kp"" type=""math_number"">
            <field name=""NUM"">2</field>
          </block>
        </value>
        <value name=""TO"">
          <block id=""0ZfyZ~oP1n6u%WA:U6|n"" type=""math_number"">
            <field name=""NUM"">30</field>
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
      Assert.IsTrue(code.Contains("dynamic list; list = new List<dynamic>{10, 20}; list[2 - 1] = 30;"));
    }

    [TestMethod]
    public void Test_Lists_SetIndex_FromEnd()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""cc|GVfJ3tvn4)$KU@H7y"" type="""">list</variable>
  </variables>
  <block id=""fR5O0Jp|d1OmOK-Pk^_P"" type=""variables_set"" x=""288"" y=""88"">
    <field id=""cc|GVfJ3tvn4)$KU@H7y"" name=""VAR"" variabletype="""">list</field>
    <value name=""VALUE"">
      <block id=""!bX1D@bA^X|EPq~|rE@8"" type=""lists_create_with"">
        <mutation items=""0""></mutation>
      </block>
    </value>
    <next>
      <block id=""C7rv=SPiJD~wmb3i~`.Q"" type=""lists_setIndex"">
        <mutation at=""true""></mutation>
        <field name=""MODE"">SET</field>
        <field name=""WHERE"">FROM_END</field>
        <value name=""LIST"">
          <block id=""5VIlE8Inpe_qTmvAZEV}"" type=""variables_get"">
            <field id=""cc|GVfJ3tvn4)$KU@H7y"" name=""VAR"" variabletype="""">list</field>
          </block>
        </value>
        <value name=""AT"">
          <block id=""T1}cABG9FDd@lwW:W|u^"" type=""math_number"">
            <field name=""NUM"">2</field>
          </block>
        </value>
        <value name=""TO"">
          <block id=""/T$p;AuHvxym5s4k:jC7"" type=""math_number"">
            <field name=""NUM"">10</field>
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
      Assert.IsTrue(code.Contains("dynamic list; list = new List<dynamic>{}; list[list.Count - 2] = 10;"));
    }

    [TestMethod]
    public void Test_Lists_SetIndex_First()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""cc|GVfJ3tvn4)$KU@H7y"" type="""">list</variable>
  </variables>
  <block id=""fR5O0Jp|d1OmOK-Pk^_P"" type=""variables_set"" x=""288"" y=""88"">
    <field id=""cc|GVfJ3tvn4)$KU@H7y"" name=""VAR"" variabletype="""">list</field>
    <value name=""VALUE"">
      <block id=""!bX1D@bA^X|EPq~|rE@8"" type=""lists_create_with"">
        <mutation items=""0""></mutation>
      </block>
    </value>
    <next>
      <block id=""C7rv=SPiJD~wmb3i~`.Q"" type=""lists_setIndex"">
        <mutation at=""false""></mutation>
        <field name=""MODE"">SET</field>
        <field name=""WHERE"">FIRST</field>
        <value name=""LIST"">
          <block id=""5VIlE8Inpe_qTmvAZEV}"" type=""variables_get"">
            <field id=""cc|GVfJ3tvn4)$KU@H7y"" name=""VAR"" variabletype="""">list</field>
          </block>
        </value>
        <value name=""TO"">
          <block id=""/T$p;AuHvxym5s4k:jC7"" type=""math_number"">
            <field name=""NUM"">10</field>
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
      Assert.IsTrue(code.Contains("dynamic list; list = new List<dynamic>{}; list[0] = 10;"));
    }

    [TestMethod]
    public void Test_Lists_SetIndex_Last()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""cc|GVfJ3tvn4)$KU@H7y"" type="""">list</variable>
  </variables>
  <block id=""fR5O0Jp|d1OmOK-Pk^_P"" type=""variables_set"" x=""288"" y=""88"">
    <field id=""cc|GVfJ3tvn4)$KU@H7y"" name=""VAR"" variabletype="""">list</field>
    <value name=""VALUE"">
      <block id=""!bX1D@bA^X|EPq~|rE@8"" type=""lists_create_with"">
        <mutation items=""0""></mutation>
      </block>
    </value>
    <next>
      <block id=""C7rv=SPiJD~wmb3i~`.Q"" type=""lists_setIndex"">
        <mutation at=""false""></mutation>
        <field name=""MODE"">SET</field>
        <field name=""WHERE"">LAST</field>
        <value name=""LIST"">
          <block id=""5VIlE8Inpe_qTmvAZEV}"" type=""variables_get"">
            <field id=""cc|GVfJ3tvn4)$KU@H7y"" name=""VAR"" variabletype="""">list</field>
          </block>
        </value>
        <value name=""TO"">
          <block id=""/T$p;AuHvxym5s4k:jC7"" type=""math_number"">
            <field name=""NUM"">10</field>
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
      Assert.IsTrue(code.Contains("dynamic list; list = new List<dynamic>{}; list[list.Count - 1] = 10;"));
    }
  }
}
