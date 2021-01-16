using System;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Math
{
  public class MathArithmetic : IBlock
  {
    public override object Evaluate(Context context)
    {
      var a = (double)this.Values.Evaluate("A", context);
      var b = (double)this.Values.Evaluate("B", context);

      var opValue = this.Fields.Get("OP");

      switch (opValue)
      {
        case "MULTIPLY": return a * b;
        case "DIVIDE": return a / b;
        case "ADD": return a + b;
        case "MINUS": return a - b;
        case "POWER": return System.Math.Pow(a, b);

        default: throw new ApplicationException($"Unknown OP {opValue}");
      }
    }

    public override SyntaxNode Generate(Context context)
    {
      var firstExpression = this.Values.Generate("A", context) as ExpressionSyntax;
      if (firstExpression == null) throw new ApplicationException($"Unknown expression for value A.");

      var secondExpression = this.Values.Generate("B", context) as ExpressionSyntax;
      if (secondExpression == null) throw new ApplicationException($"Unknown expression for value B.");

      ExpressionSyntax expression = null;

      var opValue = this.Fields.Get("OP");
      if (opValue == "POWER")
      {
        expression =
          SyntaxGenerator.MethodInvokeExpression(
            IdentifierName(nameof(System.Math)),
            nameof(System.Math.Pow),
            new[] { firstExpression, secondExpression }
          );
      }
      else
      {
        var binaryOperator = GetBinaryOperator(opValue);
        expression = BinaryExpression(binaryOperator, firstExpression, secondExpression);
      }

      return ParenthesizedExpression(expression);
    }

    private SyntaxKind GetBinaryOperator(string opValue)
    {
      switch (opValue)
      {
        case "MULTIPLY": return SyntaxKind.MultiplyExpression;
        case "DIVIDE": return SyntaxKind.DivideExpression;
        case "ADD": return SyntaxKind.AddExpression;
        case "MINUS": return SyntaxKind.SubtractExpression;

        default: throw new ApplicationException($"Unknown OP {opValue}");
      }
    }
  }

}