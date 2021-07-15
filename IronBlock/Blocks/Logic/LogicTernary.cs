using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Logic
{
  public class LogicTernary : IBlock
  {
    public override object Evaluate(Context context)
    {
      var ifValue = (bool)this.Values.Evaluate("IF", context);

      if (ifValue)
      {
        if (this.Values.Any(x => x.Name == "THEN"))
        {
          return this.Values.Evaluate("THEN", context);
        }
      }
      else
      {
        if (this.Values.Any(x => x.Name == "ELSE"))
        {
          return this.Values.Generate("ELSE", context);
        }
      }
      return null;
    }
    public override SyntaxNode Generate(Context context)
    {
      var conditionalExpression = this.Values.Generate("IF", context) as ExpressionSyntax;
      if (conditionalExpression == null) throw new ApplicationException($"Unknown expression for conditional statement.");

      var trueExpression = this.Values.Generate("THEN", context) as ExpressionSyntax;
      if (trueExpression == null) throw new ApplicationException($"Unknown expression for true statement.");

      var falseExpression = this.Values.Generate("ELSE", context) as ExpressionSyntax;
      if (falseExpression == null) throw new ApplicationException($"Unknown expression for false statement.");

      return ConditionalExpression(
            conditionalExpression,
            trueExpression,
            falseExpression
          );
    }
  }

}