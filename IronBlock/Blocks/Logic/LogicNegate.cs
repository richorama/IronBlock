using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Logic
{
  public class LogicNegate : IBlock
  {
    public override object Evaluate(Context context)
    {
      return !((bool)(this.Values.Evaluate("BOOL", context) ?? false));
    }

    public override SyntaxNode Generate(Context context)
    {
      var boolExpression = this.Values.Generate("BOOL", context) as ExpressionSyntax;
      if (boolExpression == null) throw new ApplicationException($"Unknown expression for negate.");

      return PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, boolExpression);
    }
  }

}