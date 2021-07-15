using System;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Math
{
  public class MathSingle : IBlock
  {
    public override object Evaluate(Context context)
    {
      var op = this.Fields.Get("OP");
      var number = (double)this.Values.Evaluate("NUM", context);

      switch (op)
      {
        case "ROOT": return System.Math.Sqrt(number);
        case "ABS": return System.Math.Abs(number);
        case "NEG": return -1 * number;
        case "LN": return System.Math.Log(number);
        case "LOG10": return System.Math.Log10(number);
        case "EXP": return System.Math.Exp(number);
        case "POW10": return System.Math.Pow(number, 10);

        case "SIN": return System.Math.Sin(number / 180 * System.Math.PI);
        case "COS": return System.Math.Cos(number / 180 * System.Math.PI);
        case "TAN": return System.Math.Tan(number / 180 * System.Math.PI);
        case "ASIN": return System.Math.Asin(number / 180 * System.Math.PI);
        case "ACOS": return System.Math.Acos(number / 180 * System.Math.PI);
        case "ATAN": return System.Math.Atan(number / 180 * System.Math.PI);

        default: throw new ApplicationException($"Unknown OP {op}");
      }
    }

    public override SyntaxNode Generate(Context context)
    {
      var op = this.Fields.Get("OP");
      var numberExpression = this.Values.Generate("NUM", context) as ExpressionSyntax;
      if (numberExpression == null) throw new ApplicationException($"Unknown expression for number.");

      switch (op)
      {
        case "ROOT": return MathFunction(nameof(System.Math.Sqrt), numberExpression);
        case "ABS": return MathFunction(nameof(System.Math.Abs), numberExpression);
        case "NEG":
          return PrefixUnaryExpression(
          SyntaxKind.UnaryMinusExpression,
          numberExpression
        );

        case "LN": return MathFunction(nameof(System.Math.Log), numberExpression);
        case "LOG10": return MathFunction(nameof(System.Math.Log10), numberExpression);
        case "EXP": return MathFunction(nameof(System.Math.Exp), numberExpression);
        case "POW10":
          return MathFunction(nameof(System.Math.Pow), numberExpression)
    .WithArgumentList(
      ArgumentList(
        SeparatedList<ArgumentSyntax>(
          new SyntaxNodeOrToken[]{
                        Argument(
                          numberExpression
                        ),
                        Token(SyntaxKind.CommaToken),
                        Argument(
                          LiteralExpression(
                            SyntaxKind.NumericLiteralExpression,
                            Literal(10)
                          )
                        )
          }
        )
      )
    );

        case "SIN": return MathFunction(nameof(System.Math.Sin), DegreesToRadians(numberExpression));
        case "COS": return MathFunction(nameof(System.Math.Cos), DegreesToRadians(numberExpression));
        case "TAN": return MathFunction(nameof(System.Math.Tan), DegreesToRadians(numberExpression));
        case "ASIN": return MathFunction(nameof(System.Math.Asin), DegreesToRadians(numberExpression));
        case "ACOS": return MathFunction(nameof(System.Math.Acos), DegreesToRadians(numberExpression));
        case "ATAN": return MathFunction(nameof(System.Math.Atan), DegreesToRadians(numberExpression));

        default: throw new ApplicationException($"Unknown OP {op}");
      }
    }

    private ExpressionSyntax DegreesToRadians(ExpressionSyntax numberExpression)
    {
      return
        BinaryExpression(
          SyntaxKind.DivideExpression,
          numberExpression,
          ParenthesizedExpression(
            BinaryExpression(
              SyntaxKind.MultiplyExpression,
              LiteralExpression(
                SyntaxKind.NumericLiteralExpression,
                Literal(180)
              ),
              SyntaxGenerator.PropertyAccessExpression(
                IdentifierName(nameof(System.Math)),
                nameof(System.Math.PI)
              )
            )
          )
        );
    }

    public static InvocationExpressionSyntax MathFunction(string functionName, ExpressionSyntax numberExpression)
    {
      return SyntaxGenerator.MethodInvokeExpression(IdentifierName(nameof(System.Math)), functionName, numberExpression);
    }
  }
}