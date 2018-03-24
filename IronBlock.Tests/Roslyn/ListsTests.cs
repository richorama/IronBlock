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
			Assert.IsTrue(code.Contains("\"x,y,z\".ToString().Split(\",\");"));

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
			Assert.IsTrue(code.Contains("\"a,b,c\".ToString().Split(\",\").Any();"));
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
			Assert.IsTrue(code.Contains("Enumerable.Repeat(\"hello\", 3);"));
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
    }
}
