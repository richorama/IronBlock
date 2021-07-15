using System.Linq;
using IronBlock.Blocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests
{
  [TestClass]
  public class ControlsTests
  {
    [TestMethod]
    public void Test_Controls_If()
    {

      const string xml = @"
<xml>
  <block type=""controls_if"" >
    <value name=""IF0"">
      <block type=""logic_boolean"">
        <field name=""BOOL"">TRUE</field>
      </block>
    </value>
    <statement name=""DO0"">
      <block type=""text_print"">
        <value name=""TEXT"">
          <shadow type=""text"">
            <field name=""TEXT"">success</field>
          </shadow>
        </value>
      </block>
    </statement>
  </block>       
</xml>
";
      new Parser()
          .AddStandardBlocks()
          .AddDebugPrinter()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual("success", TestExtensions.GetDebugText().First());
    }


    [TestMethod]
    public void Test_Controls_WhileUntil()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable type="""">x</variable>
  </variables>
  <block type=""variables_set"">
    <field name=""VAR"" variabletype="""">x</field>
    <value name=""VALUE"">
      <block type=""math_number"">
        <field name=""NUM"">0</field>
      </block>
    </value>
    <next>
      <block type=""controls_whileUntil"">
        <field name=""MODE"">WHILE</field>
        <value name=""BOOL"">
          <block type=""logic_compare"">
            <field name=""OP"">EQ</field>
            <value name=""A"">
              <block type=""variables_get"">
                <field name=""VAR"" variabletype="""">x</field>
              </block>
            </value>
            <value name=""B"">
              <block type=""math_number"">
                <field name=""NUM"">0</field>
              </block>
            </value>
          </block>
        </value>
        <statement name=""DO"">
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
    </next>
  </block>
</xml>
";
      var parser = new Parser()
          .AddStandardBlocks()
          .AddDebugPrinter()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual("0", string.Join(",", TestExtensions.GetDebugText()));
    }


    [TestMethod]
    public void Test_Controls_Flow_Continue()
    {

      const string xml = @"
<xml>
  <block type=""controls_repeat_ext"">
    <value name=""TIMES"">
      <shadow type=""math_number"">
        <field name=""NUM"">3</field>
      </shadow>
    </value>
    <statement name=""DO"">
      <block type=""text_print"">
        <value name=""TEXT"">
          <shadow type=""text"">
            <field name=""TEXT"">hello</field>
          </shadow>
        </value>
        <next>
          <block type=""controls_if"">
            <value name=""IF0"">
              <block type=""logic_boolean"">
                <field name=""BOOL"">TRUE</field>
              </block>
            </value>
            <statement name=""DO0"">
              <block type=""controls_flow_statements"">
                <field name=""FLOW"">CONTINUE</field>
              </block>
            </statement>
            <next>
              <block type=""text_print"">
                <value name=""TEXT"">
                  <shadow type=""text"">
                    <field name=""TEXT"">world</field>
                  </shadow>
                </value>
              </block>
            </next>
          </block>
        </next>
      </block>
    </statement>
  </block>
</xml>
";
      new Parser()
          .AddStandardBlocks()
          .AddDebugPrinter()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual("hello,hello,hello", string.Join(",", TestExtensions.GetDebugText()));
    }


    [TestMethod]
    public void Test_Controls_Flow_Break()
    {

      const string xml = @"
<xml>
  <block type=""controls_repeat_ext"">
    <value name=""TIMES"">
      <shadow type=""math_number"">
        <field name=""NUM"">3</field>
      </shadow>
    </value>
    <statement name=""DO"">
      <block type=""text_print"">
        <value name=""TEXT"">
          <shadow type=""text"">
            <field name=""TEXT"">hello</field>
          </shadow>
        </value>
        <next>
          <block type=""controls_if"">
            <value name=""IF0"">
              <block type=""logic_boolean"">
                <field name=""BOOL"">TRUE</field>
              </block>
            </value>
            <statement name=""DO0"">
              <block type=""controls_flow_statements"">
                <field name=""FLOW"">BREAK</field>
              </block>
            </statement>
            <next>
              <block type=""text_print"">
                <value name=""TEXT"">
                  <shadow type=""text"">
                    <field name=""TEXT"">world</field>
                  </shadow>
                </value>
              </block>
            </next>
          </block>
        </next>
      </block>
    </statement>
  </block>
</xml>
";
      new Parser()
          .AddStandardBlocks()
          .AddDebugPrinter()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual("hello", string.Join(",", TestExtensions.GetDebugText()));
    }



    [TestMethod]
    public void Test_Controls_For_Each()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable type="""" id=""%%M;gt+!MJxzjuj,*~.X"">i</variable>
  </variables>
  <block type=""controls_forEach"" id=""Mue=s2=VyJ,|.^Jk7Y6$"" x=""112"" y=""112"">
    <field name=""VAR"" id=""%%M;gt+!MJxzjuj,*~.X"" variabletype="""">i</field>
    <value name=""LIST"">
      <block type=""lists_split"" id=""U@Eu{y]crRkv4qeX!wgB"">
        <mutation mode=""SPLIT""></mutation>
        <field name=""MODE"">SPLIT</field>
        <value name=""INPUT"">
          <block type=""text"" id=""JkR7d1MvrJydd#t2yr,O"">
            <field name=""TEXT"">a,b,c</field>
          </block>
        </value>
        <value name=""DELIM"">
          <shadow type=""text"" id=""M6%A!]!KJA04vggp9X8*"">
            <field name=""TEXT"">,</field>
          </shadow>
        </value>
      </block>
    </value>
    <statement name=""DO"">
      <block type=""text_print"" id="";j~?B]t;80-uv1Ef3qnZ"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""wD|J$)cg{^g4+P3!1QpW"">
            <field name=""TEXT"">abc</field>
          </shadow>
          <block type=""variables_get"" id=""Kp`QS4LS,+l.Bb0~+tx2"">
            <field name=""VAR"" id=""%%M;gt+!MJxzjuj,*~.X"" variabletype="""">i</field>
          </block>
        </value>
      </block>
    </statement>
  </block>
</xml>
";
      new Parser()
          .AddStandardBlocks()
          .AddDebugPrinter()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual("a|b|c", string.Join("|", TestExtensions.GetDebugText()));
    }


    [TestMethod]
    public void Test_Controls_For()
    {

      const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables>
    <variable type="""" id=""%%M;gt+!MJxzjuj,*~.X"">i</variable>
  </variables>
  <block type=""controls_for"" id=""d/iaO@0M8X3$3qCi@QR]"" x=""113"" y=""263"">
    <field name=""VAR"" id=""%%M;gt+!MJxzjuj,*~.X"" variabletype="""">i</field>
    <value name=""FROM"">
      <shadow type=""math_number"" id=""Rx;IYft^ona!~Skl@in`"">
        <field name=""NUM"">1</field>
      </shadow>
    </value>
    <value name=""TO"">
      <shadow type=""math_number"" id=""sl*-19-B$bU7=H3D2W`q"">
        <field name=""NUM"">3</field>
      </shadow>
    </value>
    <value name=""BY"">
      <shadow type=""math_number"" id=""I6{~_*N.9;,`_8brq`)i"">
        <field name=""NUM"">1</field>
      </shadow>
    </value>
    <statement name=""DO"">
      <block type=""text_print"" id="";j~?B]t;80-uv1Ef3qnZ"">
        <value name=""TEXT"">
          <shadow type=""text"" id=""wD|J$)cg{^g4+P3!1QpW"">
            <field name=""TEXT"">abc</field>
          </shadow>
          <block type=""variables_get"" id=""Kp`QS4LS,+l.Bb0~+tx2"">
            <field name=""VAR"" id=""%%M;gt+!MJxzjuj,*~.X"" variabletype="""">i</field>
          </block>
        </value>
      </block>
    </statement>
  </block>
</xml>
";
      new Parser()
          .AddStandardBlocks()
          .AddDebugPrinter()
          .Parse(xml)
          .Evaluate();

      Assert.AreEqual("1,2,3", string.Join(",", TestExtensions.GetDebugText()));
    }


  }
}
