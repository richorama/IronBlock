using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Logic
{
  public class LogicCompare : IBlock
  {
    public override object Evaluate(Context context)
    {
      var a = this.Values.Evaluate("A", context);
      var b = this.Values.Evaluate("B", context);

      var opValue = this.Fields.Get("OP");

      if (a is string) return Compare(opValue, a as string, b as string);
      if (a is double) return Compare(opValue, (double)a, (double)b);
      if (a is bool) return Compare(opValue, (bool)a, (bool)b);

      throw new ApplicationException("unexpected value type");
    }

    public override SyntaxNode Generate(Context context)
    {
      var firstExpression = this.Values.Generate("A", context) as ExpressionSyntax;
      if (firstExpression == null) throw new ApplicationException($"Unknown expression for value A.");

      var secondExpression = this.Values.Generate("B", context) as ExpressionSyntax;
      if (secondExpression == null) throw new ApplicationException($"Unknown expression for value B.");

      var opValue = this.Fields.Get("OP");

      var binaryOperator = GetBinaryOperator(opValue);
      var expression = BinaryExpression(binaryOperator, firstExpression, secondExpression);

      return ParenthesizedExpression(expression);
    }

    bool Compare(string op, string a, string b)
    {
      switch (op)
      {
        case "EQ": return a == b;
        case "NEQ": return a != b;
        case "LT": return string.Compare(a, b) < 0;
        case "LTE": return string.Compare(a, b) <= 0;
        case "GT": return string.Compare(a, b) > 0;
        case "GTE": return string.Compare(a, b) >= 0;
        default: throw new ApplicationException($"Unknown OP {op}");
      }
    }

    bool Compare(string op, double a, double b)
    {
      switch (op)
      {
        case "EQ": return a == b;
        case "NEQ": return a != b;
        case "LT": return a < b;
        case "LTE": return a <= b;
        case "GT": return a > b;
        case "GTE": return a >= b;
        default: throw new ApplicationException($"Unknown OP {op}");
      }
    }

    bool Compare(string op, bool a, bool b)
    {
      switch (op)
      {
        case "EQ": return a == b;
        case "NEQ": return a != b;
        case "LT": return Convert.ToByte(a) < Convert.ToByte(b);
        case "LTE": return Convert.ToByte(a) <= Convert.ToByte(b);
        case "GT": return Convert.ToByte(a) > Convert.ToByte(b);
        case "GTE": return Convert.ToByte(a) >= Convert.ToByte(b);
        default: throw new ApplicationException($"Unknown OP {op}");
      }
    }

    private SyntaxKind GetBinaryOperator(string opValue)
    {
      switch (opValue)
      {
        case "EQ": return SyntaxKind.EqualsExpression;
        case "NEQ": return SyntaxKind.NotEqualsExpression;
        case "LT": return SyntaxKind.LessThanExpression;
        case "LTE": return SyntaxKind.LessThanOrEqualExpression;
        case "GT": return SyntaxKind.GreaterThanExpression;
        case "GTE": return SyntaxKind.GreaterThanOrEqualExpression;

        default: throw new ApplicationException($"Unknown OP {opValue}");
      }
    }
  }
}