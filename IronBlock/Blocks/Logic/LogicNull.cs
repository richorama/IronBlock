using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Logic
{
  public class LogicNull : IBlock
  {
    public override object Evaluate(Context context)
    {
      return null;
    }

    public override SyntaxNode Generate(Context context)
    {
      return ReturnStatement(
            LiteralExpression(
              SyntaxKind.NullLiteralExpression
            )
          );
    }
  }

}