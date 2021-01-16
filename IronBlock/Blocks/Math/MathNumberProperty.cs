using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Math
{
  public class MathNumberProperty : IBlock
  {
    public override object Evaluate(Context context)
    {
      var op = this.Fields.Get("PROPERTY");
      var number = (double)this.Values.Evaluate("NUMBER_TO_CHECK", context);

      switch (op)
      {
        case "EVEN": return 0 == number % 2.0;
        case "ODD": return 1 == number % 2.0;
        case "PRIME": return IsPrime((int)number);
        case "WHOLE": return 0 == number % 1.0;
        case "POSITIVE": return number > 0;
        case "NEGATIVE": return number < 0;
        case "DIVISIBLE_BY": return 0 == number % (double)this.Values.Evaluate("DIVISOR", context);
        default: throw new ApplicationException($"Unknown PROPERTY {op}");
      }
    }

    public override SyntaxNode Generate(Context context)
    {
      var op = this.Fields.Get("PROPERTY");
      var numberExpression = this.Values.Generate("NUMBER_TO_CHECK", context) as ExpressionSyntax;
      if (numberExpression == null) throw new ApplicationException($"Unknown expression for number.");

      switch (op)
      {
        case "EVEN":
          return CompareModulo(numberExpression, LiteralValue(2), 0);
        case "ODD":
          return CompareModulo(numberExpression, LiteralValue(2), 1);
        case "PRIME":
          throw new NotImplementedException($"OP {op} not implemented");
        case "WHOLE":
          return CompareModulo(numberExpression, LiteralValue(1), 0);
        case "POSITIVE":
          return BinaryExpression(
            SyntaxKind.GreaterThanExpression,
            numberExpression,
            LiteralValue(0)
          );
        case "NEGATIVE":
          return BinaryExpression(
            SyntaxKind.LessThanExpression,
            numberExpression,
            LiteralValue(0)
          );
        case "DIVISIBLE_BY":
          var divisorExpression = this.Values.Generate("DIVISOR", context) as ExpressionSyntax;
          if (divisorExpression == null) throw new ApplicationException($"Unknown expression for divisor.");

          return CompareModulo(numberExpression, divisorExpression, 0);
        default: throw new ApplicationException($"Unknown PROPERTY {op}");
      }
    }

    private LiteralExpressionSyntax LiteralValue(double value)
    {
      return LiteralExpression(
        SyntaxKind.NumericLiteralExpression,
        Literal(value)
      );
    }

    private SyntaxNode CompareModulo(ExpressionSyntax numberExpression, ExpressionSyntax moduloValueExpression, double compareValue)
    {
      return
        BinaryExpression(
          SyntaxKind.EqualsExpression,
          BinaryExpression(
            SyntaxKind.ModuloExpression,
            numberExpression,
            moduloValueExpression
          ),
          LiteralValue(compareValue)
        );
    }

    static bool IsPrime(int number)
    {
      if (number == 1) return false;
      if (number == 2) return true;
      if (number % 2 == 0) return false;

      var boundary = (int)System.Math.Floor(System.Math.Sqrt(number));

      for (int i = 3; i <= boundary; i += 2)
      {
        if (number % i == 0) return false;
      }

      return true;
    }

  }

}