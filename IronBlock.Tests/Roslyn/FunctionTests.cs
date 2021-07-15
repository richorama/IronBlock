using System;
using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
{
  [TestClass]
  public class FunctionTests
  {
    [TestMethod]
    public void Test_Procedure_No_Params()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""px6p$Rn#{{${?B:OIE[v"" type="""">a</variable>
  </variables>
  <block id=""h3UdCI~n5)/pHX6A1Tl4"" type=""procedures_defnoreturn"" x=""-112"" y=""-187"">
    <field name=""NAME"">init</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <statement name=""STACK"">
      <block id=""UcV0W4quSsFl/vhWi%ZB"" type=""variables_set"">
        <field id=""px6p$Rn#{{${?B:OIE[v"" name=""VAR"" variabletype="""">a</field>
        <value name=""VALUE"">
          <block id=""9[YOG`Ny4,WYK;$jo5I$"" type=""math_number"">
            <field name=""NUM"">1</field>
          </block>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("void init() { a = 1; }"));
    }

    [TestMethod]
    public void Test_Procedure_No_Params_Multiple_Statements()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""px6p$Rn#{{${?B:OIE[v"" type="""">a</variable>
  </variables>
  <block id=""h3UdCI~n5)/pHX6A1Tl4"" type=""procedures_defnoreturn"" x=""-112"" y=""-187"">
    <field name=""NAME"">init</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <statement name=""STACK"">
      <block type=""text_print"">
        <value name=""TEXT"">
            <shadow type=""text"">
            <field name=""TEXT"">abc</field>
            </shadow>
            <block type=""variables_get"">
            <field name=""VAR"" variabletype="""">x</field>
            </block>
        </value>
        <next>
            <block type=""variables_set"">
            <field name=""VAR"" variabletype="""">x</field>
            <value name=""VALUE"">
                <block type=""math_number"">
                <field name=""NUM"">1</field>
                </block>
            </value>
            </block>
        </next>
	  </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("void init() { Console.WriteLine(x); x = 1; }"));
    }


    [TestMethod]
    public void Test_Procedure_Params()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""K{.(c+Utyu,8[c|3GgNZ"" type="""">x</variable>
    <variable id=""6YNX%v)pMMi{I0^$TC~@"" type="""">y</variable>
    <variable id=""eOjB(cD9#yK@^Z`j}?}b"" type="""">a</variable>
  </variables>
  <block id=""[ws@|CAR?=,%)ShQ8Zd8"" type=""procedures_defnoreturn"" x=""163"" y=""113"">
    <mutation>
      <arg name=""x"" varId=""K{.(c+Utyu,8[c|3GgNZ""></arg>
      <arg name=""y"" varId=""6YNX%v)pMMi{I0^$TC~@""></arg>
    </mutation>
    <field name=""NAME"">add</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <statement name=""STACK"">
      <block id=""tQ~ABi}f;,T%i0cm77?*"" type=""variables_set"">
        <field id=""eOjB(cD9#yK@^Z`j}?}b"" name=""VAR"" variabletype="""">a</field>
        <value name=""VALUE"">
          <block id=""MVE0]0Y[i@c~q-;_7dEC"" type=""math_arithmetic"">
            <field name=""OP"">ADD</field>
            <value name=""A"">
              <block id=""v|3YQyog@Q[?GVXB%s1u"" type=""variables_get"">
                <field id=""K{.(c+Utyu,8[c|3GgNZ"" name=""VAR"" variabletype="""">x</field>
              </block>
            </value>
            <value name=""B"">
              <block id=""xy{GLKAKwePrf!fR[gsb"" type=""variables_get"">
                <field id=""6YNX%v)pMMi{I0^$TC~@"" name=""VAR"" variabletype="""">y</field>
              </block>
            </value>
          </block>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("void add(dynamic x, dynamic y) { a = (x + y); }"));
    }


    [TestMethod]
    public void Test_Procedure_Return()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""K{.(c+Utyu,8[c|3GgNZ"" type="""">x</variable>
    <variable id=""6YNX%v)pMMi{I0^$TC~@"" type="""">y</variable>
    <variable id=""eOjB(cD9#yK@^Z`j}?}b"" type="""">a</variable>
  </variables>
  <block id=""[ws@|CAR?=,%)ShQ8Zd8"" type=""procedures_defnoreturn"" x=""163"" y=""113"">
    <mutation>
      <arg name=""x"" varId=""K{.(c+Utyu,8[c|3GgNZ""></arg>
      <arg name=""y"" varId=""6YNX%v)pMMi{I0^$TC~@""></arg>
    </mutation>
    <field name=""NAME"">add</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
	<value name=""RETURN"">
      <block id=""MVE0]0Y[i@c~q-;_7dEC"" type=""math_arithmetic"">
        <field name=""OP"">ADD</field>
        <value name=""A"">
            <block id=""v|3YQyog@Q[?GVXB%s1u"" type=""variables_get"">
            <field id=""K{.(c+Utyu,8[c|3GgNZ"" name=""VAR"" variabletype="""">x</field>
            </block>
        </value>
        <value name=""B"">
            <block id=""xy{GLKAKwePrf!fR[gsb"" type=""variables_get"">
            <field id=""6YNX%v)pMMi{I0^$TC~@"" name=""VAR"" variabletype="""">y</field>
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
      Assert.IsTrue(code.Contains("dynamic add(dynamic x, dynamic y) { return (x + y); }"));
    }

    [TestMethod]
    public void Test_Procedure_Return_With_Statements()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""/nfn$6v1kaV^fYdm2o+X"" type="""">x</variable>
    <variable id=""a$*^qf,V!$QH2Te:HGKi"" type="""">y</variable>
    <variable id=""px6p$Rn#{{${?B:OIE[v"" type="""">a</variable>
  </variables>
  <block id=""]^^1R+i38}8?G#.PyX_s"" type=""procedures_defreturn"" x=""63"" y=""63"">
    <mutation>
      <arg name=""x"" varId=""/nfn$6v1kaV^fYdm2o+X""></arg>
      <arg name=""y"" varId=""a$*^qf,V!$QH2Te:HGKi""></arg>
    </mutation>
    <field name=""NAME"">do something</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <statement name=""STACK"">
      <block id=""Wnj49+_QDK^5[u.84uRh"" type=""variables_set"">
        <field id=""px6p$Rn#{{${?B:OIE[v"" name=""VAR"" variabletype="""">a</field>
        <value name=""VALUE"">
          <block id=""o{%XdYKe|G}_C7pY6%6N"" type=""math_arithmetic"">
            <field name=""OP"">MULTIPLY</field>
            <value name=""A"">
              <block id=""X63C2K-p4na${I75]{8p"" type=""variables_get"">
                <field id=""/nfn$6v1kaV^fYdm2o+X"" name=""VAR"" variabletype="""">x</field>
              </block>
            </value>
            <value name=""B"">
              <block id=""JFz}mu,*R?l,H,L+1qJJ"" type=""variables_get"">
                <field id=""a$*^qf,V!$QH2Te:HGKi"" name=""VAR"" variabletype="""">y</field>
              </block>
            </value>
          </block>
        </value>
        <next>
          <block id=""uUA@2.]x4v}W!$8Ca%iX"" type=""variables_set"">
            <field id=""px6p$Rn#{{${?B:OIE[v"" name=""VAR"" variabletype="""">a</field>
            <value name=""VALUE"">
              <block id=""pv0ey5l6w*BmJAz]Zr|o"" type=""math_arithmetic"">
                <field name=""OP"">POWER</field>
                <value name=""A"">
                  <block id=""H~@|[p--*%}!Gb|H!#m("" type=""variables_get"">
                    <field id=""px6p$Rn#{{${?B:OIE[v"" name=""VAR"" variabletype="""">a</field>
                  </block>
                </value>
                <value name=""B"">
                  <block id=""dk#]/CBP;|)o-5#D4/(_"" type=""variables_get"">
                    <field id=""/nfn$6v1kaV^fYdm2o+X"" name=""VAR"" variabletype="""">x</field>
                  </block>
                </value>
              </block>
            </value>
          </block>
        </next>
      </block>
    </statement>
    <value name=""RETURN"">
      <block id=""4rVQ9|s-y%7J@D0?5!rG"" type=""math_arithmetic"">
        <field name=""OP"">ADD</field>
        <value name=""A"">
          <block id="":Z)x[3.h[e$l4#yauLPe"" type=""variables_get"">
            <field id=""/nfn$6v1kaV^fYdm2o+X"" name=""VAR"" variabletype="""">x</field>
          </block>
        </value>
        <value name=""B"">
          <shadow id=""A;_B`/dB]`)GBM|0;AqC"" type=""math_number"">
            <field name=""NUM"">1</field>
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

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("dynamic do_something(dynamic x, dynamic y) { a = (x * y); a = (Math.Pow(a, x)); return (x + 1); }"));
    }

    [TestMethod]
    public void Test_Procedure_If_Return()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables></variables>
  <block id=""tA[-yuXcF=|C]j8K_wTr"" type=""procedures_defnoreturn"" x=""-187"" y=""-412"">
    <field name=""NAME"">returns</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <statement name=""STACK"">
      <block id=""Hdz;|LSc)Pf|0:^L^j`y"" type=""procedures_ifreturn"">
        <mutation value=""0""></mutation>
        <value name=""CONDITION"">
          <block id=""e,xb+LH?qCC~.((Up(0."" type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("void returns() { if (true) return; }"));
    }

    [TestMethod]
    public void Test_Procedure_If_Return_Value()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""procedures_defreturn"">
    <field name=""NAME"">conditionalreturn</field>
    <comment pinned=""false"">Describe this function...</comment>
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
            <field name=""TEXT"">hello world</field>
          </block>
        </value>
      </block>
    </statement>
    <value name=""RETURN"">
      <block type=""text"">
        <field name=""TEXT"">xxx</field>
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
      Assert.IsTrue(code.Contains("dynamic conditionalreturn() { if (true) return \"hello world\"; return \"xxx\"; }"));
    }

    [TestMethod]
    public void Test_Procedure_Call_No_Return_No_Params()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <block id=""ZVn,OV`|8tZ}@ufT;E2M"" type=""procedures_callnoreturn"" x=""-187"" y=""-312"">
    <mutation name=""add""></mutation>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("add();"));
    }


    [TestMethod]
    public void Test_Procedure_Call_No_Return_Params()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <block id=""ZVn,OV`|8tZ}@ufT;E2M"" type=""procedures_callnoreturn"" x=""-237"" y=""-287"">
    <mutation name=""add"">
      <arg name=""x""></arg>
      <arg name=""y""></arg>
    </mutation>
    <value name=""ARG0"">
      <block id=""]Hy,^FgvdF`ow$yTe6*V"" type=""variables_get"">
        <field id=""G3L7~94ROOIBnpYy5vpP"" name=""VAR"" variabletype="""">a</field>
      </block>
    </value>
    <value name=""ARG1"">
      <block id=""nDTLRV*(C-iEsfz:_]gg"" type=""math_number"">
        <field name=""NUM"">1</field>
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
      Assert.IsTrue(code.Contains("add(a, 1);"));
    }

    [TestMethod]
    public void Test_Procedure_Call_Return()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <block id=""6k9heD6pi%ztj@ft.r3:"" type=""variables_set"" x=""-237"" y=""-12"">
    <field id=""G3L7~94ROOIBnpYy5vpP"" name=""VAR"" variabletype="""">a</field>
    <value name=""VALUE"">
      <block id="":MRty8i*Kb!X?(VcPXpz"" type=""procedures_callreturn"">
        <mutation name=""add"">
          <arg name=""x""></arg>
          <arg name=""y""></arg>
        </mutation>
        <value name=""ARG0"">
          <block id=""]Hy,^FgvdF`ow$yTe6*V"" type=""variables_get"">
            <field id=""G3L7~94ROOIBnpYy5vpP"" name=""VAR"" variabletype="""">a</field>
          </block>
        </value>
        <value name=""ARG1"">
          <block id=""nDTLRV*(C-iEsfz:_]gg"" type=""math_number"">
            <field name=""NUM"">1</field>
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
      Assert.IsTrue(code.Contains("a = add(a, 1);"));
    }

    [TestMethod]
    public void Test_Procedure_Call_With_Next_Blocks()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable id=""dMG6oKpWbgrw(IZWtJ,K"" type="""">result</variable>
  </variables>
  <block id=""2(,el)x8wxO00%uQeVpR"" type=""procedures_defnoreturn"" y=""-137"" x=""-1212"">
    <field name=""NAME"">do something</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
  </block>
  <block id=""gL.=CY_}N5X5J(|cB2TW"" type=""procedures_callnoreturn"" y=""-62"" x=""-1212"">
    <mutation name=""do something""></mutation>
    <next>
      <block id=""8f0,x;DF5i=kYS=@*dQ8"" type=""variables_set"">
        <field id=""dMG6oKpWbgrw(IZWtJ,K"" name=""VAR"" variabletype="""">result</field>
        <value name=""VALUE"">
          <block id=""1e*#EfQV?Od5_-7[%ozh"" type=""math_number"">
            <field name=""NUM"">123</field>
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
      Assert.IsTrue(code.Contains("do_something(); result = 123;"));
    }

    [TestMethod]
    public void Test_Procedure_If_Return_With_Next_Blocks()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable id=""dMG6oKpWbgrw(IZWtJ,K"" type="""">result</variable>
  </variables>
  <block id=""2(,el)x8wxO00%uQeVpR"" type=""procedures_defnoreturn"" y=""-162"" x=""-1187"">
    <field name=""NAME"">do something</field>
    <comment pinned=""false"" h=""80"" w=""160"">Describe this function...</comment>
    <statement name=""STACK"">
      <block id=""=4}!M(:0t9tFE2`(;UEZ"" type=""procedures_ifreturn"">
        <mutation value=""0""></mutation>
        <value name=""CONDITION"">
          <block id=""o2Z]dxf9BAVBcb0#z=/E"" type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
        <next>
          <block id=""8f0,x;DF5i=kYS=@*dQ8"" type=""variables_set"">
            <field id=""dMG6oKpWbgrw(IZWtJ,K"" name=""VAR"" variabletype="""">result</field>
            <value name=""VALUE"">
              <block id=""1e*#EfQV?Od5_-7[%ozh"" type=""math_number"">
                <field name=""NUM"">123</field>
              </block>
            </value>
          </block>
        </next>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
          .AddStandardBlocks()
          .Parse(xml)
          .Generate();

      string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
      Assert.IsTrue(code.Contains("if (true) return; result = 123;"));
    }
  }
}

