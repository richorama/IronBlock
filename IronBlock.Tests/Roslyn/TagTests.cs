using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
{
    [TestClass]
    public class TagTests
    {	
		[TestMethod]
		public void Test_Tag_Get()
		{
			const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables></variables>
  <block type=""math_arithmetic"" id=""DLoe=XGQNc6Q*+.K-^xL"" x=""144"" y=""76"">
    <field name=""OP"">ADD</field>
    <value name=""A"">
      <block type=""connectitude_tags_get"" id="";FPDW=)|EXoQLY9N?ed*"">
        <field name=""TAG"">3514A5CA-67F2-4A84-9F54-004820575636</field>
      </block>
    </value>
    <value name=""B"">
      <block type=""connectitude_tags_get"" id=""V8]x2}Ab-QXHse{,eajl"">
        <field name=""TAG"">B5F5962E-D084-4359-97F2-01A0B94C23AB</field>
      </block>
    </value>
  </block>
</xml>";

			var output = new Parser()
				.AddStandardBlocks()
				.Parse(xml)
				.Generate();

			string code = output.NormalizeWhitespace().ToFullString();
			Assert.IsTrue(code.Contains("(_3514A5CA_67F2_4A84_9F54_004820575636 + _B5F5962E_D084_4359_97F2_01A0B94C23AB);"));
		}
	}
}
