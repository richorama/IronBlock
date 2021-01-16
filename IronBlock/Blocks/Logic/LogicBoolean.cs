using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Logic
{
  public class LogicBoolean : IBlock
  {
    public override object Evaluate(Context context)
    {
      return bool.Parse(this.Fields.Get("BOOL"));
    }

    public override SyntaxNode Generate(Context context)
    {
      bool value = bool.Parse(this.Fields.Get("BOOL"));
      if (value)
        return LiteralExpression(SyntaxKind.TrueLiteralExpression);

      return LiteralExpression(SyntaxKind.FalseLiteralExpression);
    }
  }
}