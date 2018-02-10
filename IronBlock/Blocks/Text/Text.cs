using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;

namespace IronBlock.Blocks.Text
{
    public class TextBlock : IBlock
    {
        public override object Evaluate(Context context)
        {
            var text = this.Fields.Get("TEXT");

            return text;
        }

        public override SyntaxNode Generate(Context context)
        {
            var variableName = this.Fields.Get("TEXT");

            return SyntaxFactory.IdentifierName(variableName);
        }
    }

}