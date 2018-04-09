using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace IronBlock.Tests.Roslyn
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
			var output = new Parser()
				.AddStandardBlocks()
				.Parse(xml)
				.Generate();

			string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
			Assert.IsTrue(code.Contains("if (true) { Console.WriteLine(\"success\"); }"));
		}
		
		[TestMethod]
		public void Test_Controls_If_Else()
		{
			const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""7seVex*HL`I8akO#+j43"" type="""">test</variable>
  </variables>
  <block id=""Mx`4+~I;[c(#@zi=m_v5"" type=""controls_if"" x=""88"" y=""88"">
    <mutation elseif=""0"" else=""1""></mutation>
    <value name=""IF0"">
      <block id=""#H5#J{dHKLgR[?[l3E)E"" type=""logic_compare"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block id=""Q!~(XOMXDpU0m1P~0xes"" type=""variables_get"">
            <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
          </block>
        </value>
        <value name=""B"">
          <block id=""c2-7jShYtMs)HB4oF~}9"" type=""math_number"">
            <field name=""NUM"">0</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block id=""Y,_0iMWG9+{u3BfSFMz5"" type=""variables_set"">
        <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
        <value name=""VALUE"">
          <block id=""#nOFET/#9ukLb.)L,wBn"" type=""math_number"">
            <field name=""NUM"">1</field>
          </block>
        </value>
      </block>
    </statement>
    <statement name=""ELSE"">
      <block id=""c]*H;AGQX2ZwRx:jf*Ds"" type=""variables_set"">
        <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
        <value name=""VALUE"">
          <block id=""PeXb.]r/z~Au:q8s-!a2"" type=""math_number"">
            <field name=""NUM"">2</field>
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
			Assert.IsTrue(code.Contains("if ((test == 0)) { test = 1; } else { test = 2; }"));
		}

		[TestMethod]
		public void Test_Controls_If_ElseIf_Else()
		{

			const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"" id=""workspaceBlocks"" style=""display:none"">
  <variables>
    <variable id=""7seVex*HL`I8akO#+j43"" type="""">test</variable>
  </variables>
  <block id=""Mx`4+~I;[c(#@zi=m_v5"" type=""controls_if"" x=""88"" y=""88"">
    <mutation elseif=""2"" else=""1""></mutation>
    <value name=""IF0"">
      <block id=""#H5#J{dHKLgR[?[l3E)E"" type=""logic_compare"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block id=""Q!~(XOMXDpU0m1P~0xes"" type=""variables_get"">
            <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
          </block>
        </value>
        <value name=""B"">
          <block id=""c2-7jShYtMs)HB4oF~}9"" type=""math_number"">
            <field name=""NUM"">0</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO0"">
      <block id=""Y,_0iMWG9+{u3BfSFMz5"" type=""variables_set"">
        <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
        <value name=""VALUE"">
          <block id=""#nOFET/#9ukLb.)L,wBn"" type=""math_number"">
            <field name=""NUM"">1</field>
          </block>
        </value>
      </block>
    </statement>
    <value name=""IF1"">
      <block id=""_}}wAMj6fs+cFDp}bBD^"" type=""logic_compare"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block id=""O`|hJBs}x_-;kvUIqL$G"" type=""variables_get"">
            <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
          </block>
        </value>
        <value name=""B"">
          <block id="")cfY$Kq.80SObDPf_9!R"" type=""math_number"">
            <field name=""NUM"">1</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO1"">
      <block id=""1T*.}%rbnn[]*g?73[(3"" type=""variables_set"">
        <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
        <value name=""VALUE"">
          <block id=""cEgr4f[~/hB3`4rVpp2A"" type=""math_number"">
            <field name=""NUM"">2</field>
          </block>
        </value>
      </block>
    </statement>
    <value name=""IF2"">
      <block id=""iLjW)Z@(MrZrx-vgh=Y1"" type=""logic_compare"">
        <field name=""OP"">EQ</field>
        <value name=""A"">
          <block id="")[{GuJ2K@.@Dtkj-Wxsd"" type=""variables_get"">
            <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
          </block>
        </value>
        <value name=""B"">
          <block id=""Ch!x@F#T%Tn6|zNZbQV9"" type=""math_number"">
            <field name=""NUM"">2</field>
          </block>
        </value>
      </block>
    </value>
    <statement name=""DO2"">
      <block id=""Zp{|Oc+1e#OMKWw4@F2O"" type=""variables_set"">
        <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
        <value name=""VALUE"">
          <block id=""ePq1.$X}-Er}]:sA|!q."" type=""math_number"">
            <field name=""NUM"">3</field>
          </block>
        </value>
      </block>
    </statement>
    <statement name=""ELSE"">
      <block id=""c]*H;AGQX2ZwRx:jf*Ds"" type=""variables_set"">
        <field id=""7seVex*HL`I8akO#+j43"" name=""VAR"" variabletype="""">test</field>
        <value name=""VALUE"">
          <block id=""PeXb.]r/z~Au:q8s-!a2"" type=""math_number"">
            <field name=""NUM"">4</field>
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
			Assert.IsTrue(code.Contains("if ((test == 0)) { test = 1; } else if ((test == 1)) { test = 2; } else if ((test == 2)) { test = 3; } else { test = 4; }"));
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
            var output = new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Generate();

			string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
			Assert.IsTrue(code.Contains("dynamic x; x = 0; while ((x == 0)) { Console.WriteLine(x); x = 1; }"));
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
			var output = new Parser()
				.AddStandardBlocks()
				.Parse(xml)
				.Generate();

			string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
			Assert.IsTrue(code.Contains("for (int count = 0; count < 3; count++) { Console.WriteLine(\"hello\"); if (true) { continue; }  Console.WriteLine(\"world\"); }"));
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
			var output = new Parser()
				.AddStandardBlocks()
				.Parse(xml)
				.Generate();

			string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
			Assert.IsTrue(code.Contains("for (int count = 0; count < 3; count++) { Console.WriteLine(\"hello\"); if (true) { break; }  Console.WriteLine(\"world\"); }"));
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
			var output = new Parser()
				.AddStandardBlocks()
				.Parse(xml)
				.Generate();

			string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
			Assert.IsTrue(code.Contains(@"foreach (var i in ""a,b,c"".ToString(CultureInfo.InvariantCulture).Split("","")) { Console.WriteLine(i); }"));
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
			var output = new Parser()
				.AddStandardBlocks()
				.Parse(xml)
				.Generate();

			string code = output.NormalizeWhitespace(string.Empty, " ").ToFullString();
			Assert.IsTrue(code.Contains("dynamic i; for (i = 1; i <= 3; i += 1) { Console.WriteLine(i); }"));
		}
    }
}
