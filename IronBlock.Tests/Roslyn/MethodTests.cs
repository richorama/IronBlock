using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronBlock.Tests.Roslyn
{
    [TestClass]
    public class MethodTests
    {	
		[TestMethod]
		public void Test_Invoke_Method()
		{
			const string xml = @"
<xml xmlns=""http://www.w3.org/1999/xhtml"">
  <variables></variables>
  <block type=""connectitude_methods_callnoreturn"" id=""VgerH34*s*xI5Ee]l5X("" x=""226"" y=""178"">
    <mutation modem_argument=""true""></mutation>
    <field name=""METHOD_NAME"">SetupModem</field>
    <value name=""SETUP_MODEM_ARGUMENT"">
      <block type=""connectitude_tags_get"" id=""bzs);qh_6TQjjjLYodp."">
        <field name=""TAG"">0C3D2184-1037-4A59-B0B1-08292B9CFA11</field>
      </block>
    </value>
  </block>
</xml>";

			var output = new Parser()
				.AddStandardBlocks()
				.Parse(xml)
				.Generate();

			string code = output.NormalizeWhitespace().ToFullString();
			Assert.IsTrue(code.Contains("SetupModem(_0C3D2184_1037_4A59_B0B1_08292B9CFA11);"));
		}
	}
}
