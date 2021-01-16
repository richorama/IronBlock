using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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
      var text = this.Fields.Get("TEXT");

      return LiteralExpression(
          SyntaxKind.StringLiteralExpression,
            Literal(text)
          );
    }
  }

}