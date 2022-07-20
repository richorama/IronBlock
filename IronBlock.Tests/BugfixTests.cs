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
<xml>
	<variables>
		<variable id=""`cXm$e:+$_$xfnckvbWS"">n</variable>
	</variables>
	<block type=""variables_set"" id=""set_n_initial"" inline=""true"" x=""20"" y=""20"">
		<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
		<value name=""VALUE"">
			<block type=""math_number"" id=""a!$B~u(M%g2G4*QmO1y3"">
				<field name=""NUM"">1</field>
			</block>
		</value>
		<next>
			<block type=""controls_repeat_ext"" id=""repeat"" inline=""true"">
				<value name=""TIMES"">
					<block type=""math_number"" id=""p^-}JUBIABX?i~]F1N-/"">
						<field name=""NUM"">4</field>
					</block>
				</value>
				<statement name=""DO"">
					<block type=""text_print"" id=""^RMKi5lSA[4TL*d*;L*Z"">
						<value name=""TEXT"">
							<shadow type=""text"" id=""y*c#?1$[$C$r3}qRXP}1"">
								<field name=""TEXT"">abc</field>
							</shadow>
						</value>
						<next>
							<block type=""variables_set"" id=""set_n_update"" inline=""true"">
								<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
								<value name=""VALUE"">
									<block type=""math_arithmetic"" id="")[%d2l/(JaEViKo{buBb"">
										<field name=""OP"">MULTIPLY</field>
										<value name=""A"">
											<block type=""variables_get"" id=""b$;3hwIPA)fT|oYmmZ)n"">
												<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
											</block>
										</value>
										<value name=""B"">
											<block type=""math_number"" id=""t4I1,iM2QW[SIzG1crQ|"">
												<field name=""NUM"">2</field>
											</block>
										</value>
									</block>
								</value>
								<next>
									<block type=""text_print"" id=""print"">
										<value name=""TEXT"">
											<block type=""variables_get"" id=""x/o@wI:02,lq[rfI^Ck!"">
												<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
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
	<block type=""variables_set"" id=""hf^H|d299JOPBLCc(:rP"" inline=""true"" x=""20"" y=""20"">
		<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
		<value name=""VALUE"">
			<block type=""math_number"" id=""MRY1{5.QLm?o=hU|i0.-"">
				<field name=""NUM"">1</field>
			</block>
		</value>
		<next>
			<block type=""controls_repeat_ext"" id=""lpOt88AZXfNG:zje_+!g"" inline=""true"">
				<value name=""TIMES"">
					<block type=""math_number"" id=""1z/hne],iy!~-viM`=b{"">
						<field name=""NUM"">4</field>
					</block>
				</value>
				<statement name=""DO"">
					<block type=""variables_set"" id=""?1(+A-kCHz-3jB?8TNy,"" inline=""true"">
						<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
						<value name=""VALUE"">
							<block type=""math_arithmetic"" id=""-0]!]_Zs5$h%EZJuch$b"">
								<field name=""OP"">MULTIPLY</field>
								<value name=""A"">
									<block type=""variables_get"" id=""sS,,-mQyc$FHh.0pb{fy"">
										<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
									</block>
								</value>
								<value name=""B"">
									<block type=""math_number"" id=""j/9Xmj=OjAsk)Xi_/1Il"">
										<field name=""NUM"">2</field>
									</block>
								</value>
							</block>
						</value>
						<next>
							<block type=""text_print"" id=""NUNJ?=p*cQ0,}]q+PA]p"">
								<value name=""TEXT"">
									<block type=""variables_get"" id=""V0tg(?r{sn{?{WHsNTzM"">
										<field name=""VAR"" id=""`cXm$e:+$_$xfnckvbWS"">n</field>
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
        .AddDebugPrinter()
        .Parse(xml)
        .Evaluate();

      Assert.AreEqual("abc", TestExtensions.GetDebugText().First());
      Assert.AreEqual(12, TestExtensions.GetDebugText().Count());

    }


  }
}




