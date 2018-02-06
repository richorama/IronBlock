using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Connectitude.Tags
{
    public class TagsGet : IBlock
    {		
		public override SyntaxNode Generate(Context context)
		{
			var tagId = this.Fields.Get("TAG");

			context.Tags[tagId] = tagId;

			return IdentifierName(tagId.GetVariableName());
		}
	}
}