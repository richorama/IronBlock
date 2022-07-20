using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  [TestClass]
  public class BugfixTests
  {
    [TestMethod]
    public void Issue_17()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""controls_if"" id=""`Du+(_vc^5di^SX-r)Gy"" x=""-563"" y=""-187"">
    <value name=""IF0"">
      <block type=""logic_compare"" id=""IzaPI5pFR:fkFMJ@)E]9"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block type=""math_number"" id=""?M*ou@g~A#r=O3ic;$(5"">
            <field name=""NUM"">1</field>
          </block>
        </value>
        <value name=""B"">
          <block type=""text_indexOf"" id=""G`rDOxciV.H@2e(^)d$r"">
            <field name=""END"">FIRST</field>
            <value name=""VALUE"">
              <block type=""text"" id=""ofLQvK5G,]B7,_IWMu1W"">
                <field name=""TEXT"">foo bar baz</field>
              </block>
            </value>
            <value name=""FIND"">
              <shadow type=""text"" id=""c0^@7@Cn!{_2GLo/3ki`"">
                <field name=""TEXT"">foo</field>
              </shadow>
            </value>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"" id=""vhk7~#CLW_:TTeXy#.34"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""x?1zGi1mkq)$XIq7*t_-"">
            <field name=""TEXT"">it worked</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("it worked", TestExtensions.GetDebugText().First());

    }



    [TestMethod]
    public void Issue_40()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""controls_if"" id=""`Du+(_vc^5di^SX-r)Gy"" x=""-563"" y=""-187"">
    <value name=""IF0"">
      <block type=""logic_compare"" id=""IzaPI5pFR:fkFMJ@)E]9"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
        <value name=""B"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"" id=""vhk7~#CLW_:TTeXy#.34"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""x?1zGi1mkq)$XIq7*t_-"">
            <field name=""TEXT"">it worked</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("it worked", TestExtensions.GetDebugText().First());

    }

    [TestMethod]
    public void Issue_44()
    {

      const string xml = @"<xml xmlns=""https://developers.google.com/blockly/xml"">
  <block type=""controls_whileUntil"" id=""?NVqr1Vh{l~tzFs^`s#H"" x=""613"" y=""188"">
    <field name=""MODE"">WHILE</field>
    <value name=""BOOL"">
      <block type=""logic_boolean"" id=""7;4?+s6l??{2zT|`64r/"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
    <statement name=""DO"">
      <block type=""controls_flow_statements"" id=""GPzBz)WSSoAKey`9CPy;"">
        <field name=""FLOW"">BREAK</field>
      </block>
    </statement>
    <next>
      <block type=""text_print"" id=""j~G/}$Lqui@:Ly0^duO+"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""ztA6H1%N,in$*+.kd*3J"">
            <field name=""TEXT"">it worked</field>
          </shadow>
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

      Assert.AreEqual("it worked", TestExtensions.GetDebugText().FirstOrDefault());

    }



    [TestMethod]
    public void Issue_45_1()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""controls_if"" id=""`Du+(_vc^5di^SX-r)Gy"" x=""-563"" y=""-187"">
    <value name=""IF0"">
      <block type=""logic_compare"" id=""IzaPI5pFR:fkFMJ@)E]9"">
        <field name=""OP"">LT</field>
        <value name=""A"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">FALSE</field>
          </block>
        </value>
        <value name=""B"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"" id=""vhk7~#CLW_:TTeXy#.34"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""x?1zGi1mkq)$XIq7*t_-"">
            <field name=""TEXT"">it worked</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("it worked", TestExtensions.GetDebugText().First());

    }

    [TestMethod]
    public void Issue_45_2()
    {
      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <block type=""controls_if"" id=""`Du+(_vc^5di^SX-r)Gy"" x=""-563"" y=""-187"">
    <value name=""IF0"">
      <block type=""logic_compare"" id=""IzaPI5pFR:fkFMJ@)E]9"">
        <field name=""OP"">LTE</field>
        <value name=""A"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
        <value name=""B"">
          <block type=""logic_boolean"">
            <field name=""BOOL"">TRUE</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"" id=""vhk7~#CLW_:TTeXy#.34"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""x?1zGi1mkq)$XIq7*t_-"">
            <field name=""TEXT"">it worked</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>
</xml>
";

      var output = new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("it worked", TestExtensions.GetDebugText().First());

    }


   [TestMethod]
    public void Issue_56()
    {
      const string xml = @"
<xml xmlns=""https://developers.google.com/blockly/xml"">
  <variables>
    <variable id=""IP:;/(tRXJEtJO!l8Mo;"">n</variable>
  </variables>
  <block type=""variables_set"" id=""{SZ@a_+s~YfH@J=3UoMi"" x=""288"" y=""63"">
    <field name=""VAR"" id=""IP:;/(tRXJEtJO!l8Mo;"">n</field>
    <value name=""VALUE"">
      <block type=""math_number"" id=""qzOkX~rndpNj4aVP?wN3"">
        <field name=""NUM"">1</field>
      </block>
    </value>
    <next>
      <block type=""controls_repeat_ext"" id=""b4d[fzE|YdGk.iS}kE3+"">
        <value name=""TIMES"">
          <shadow type=""math_number"" id=""CJ2EX)LWTm6D}KKHKXT5"">
            <field name=""NUM"">4</field>
          </shadow>
          <block type=""math_number"" id=""i?^HN5{`6Yl(czxMsF@u"">
            <field name=""NUM"">4</field>
          </block>
        </value>
        <statement name=""DO"">
          <block type=""text_print"" id=""iYlK?K-d%R{lA~N!knWJ"">
            <value name=""TEXT"">
              <shadow type=""text"" id=""bniEQj?ova^u.87S*c%?"">
                <field name=""TEXT"">abc</field>
              </shadow>
              <block type=""variables_get"" id=""Df@rk52MD*@g8Q-`#XzI"">
                <field name=""VAR"" id=""IP:;/(tRXJEtJO!l8Mo;"">n</field>
              </block>
            </value>
            <next>
              <block type=""variables_set"" id=""@07TFca(gS5jg3%w3Xus"">
                <field name=""VAR"" id=""IP:;/(tRXJEtJO!l8Mo;"">n</field>
                <value name=""VALUE"">
                  <block type=""math_arithmetic"" id=""]$aUl34(Sn|25ZoFGEew"">
                    <field name=""OP"">MULTIPLY</field>
                    <value name=""A"">
                      <shadow type=""math_number"" id=""!|bde0V^^[rC@XI6rcO;"">
                        <field name=""NUM"">1</field>
                      </shadow>
                      <block type=""variables_get"" id=""2Ls8V@CnIEq{Xe)M8fd("">
                        <field name=""VAR"" id=""IP:;/(tRXJEtJO!l8Mo;"">n</field>
                      </block>
                    </value>
                    <value name=""B"">
                      <shadow type=""math_number"" id=""-~mp_JmuKxnrx^Z2To]i"">
                        <field name=""NUM"">1</field>
                      </shadow>
                      <block type=""math_number"" id=""tt)rqpmz@e}Y[:jpl_$:"">
                        <field name=""NUM"">2</field>
                      </block>
                    </value>
                  </block>
                </value>
                <next>
                  <block type=""text_print"" id=""l_N;(tLe@n(!C:5nzF?a"">
                    <value name=""TEXT"">
                      <shadow type=""text"" id=""c4d;6qL@,`1%FrJ7iU61"">
                        <field name=""TEXT"">abc</field>
                      </shadow>
                      <block type=""variables_get"" id=""[70Gl;b:]?~(m8_P=/$o"">
                        <field name=""VAR"" id=""IP:;/(tRXJEtJO!l8Mo;"">n</field>
                      </block>
                    </value>
                  </block>
                </next>
              </block>
            </next>
          </block>
        </statement>
      </block>
    </next>
  </block>
</xml>
";

      var output = new Parser()
        .AddStandardBlocks()
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("1", TestExtensions.GetDebugText().First());

    }


  }
}




