using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Logic
{
  public class LogicCompare : IBlock
  {
    public override object Evaluate(Context context)
    {
      string opValue = Fields.Get("OP");

      object a = Values.Evaluate("A", context);
      object b = Values.Evaluate("B", context);

      (bool canConvert, double aValue, double bValue) tryInt = TryConvertToDoubleValues(a, b); // int => blockly always uses double
      if (tryInt.canConvert)
        return Compare(opValue, tryInt.aValue, tryInt.bValue);

      (bool canConvert, double aValue, double bValue) tryDouble = TryConvertValues<double>(a, b);
      if (tryDouble.canConvert)
        return Compare(opValue, tryDouble.aValue, tryDouble.bValue);

      (bool canConvert, string aValue, string bValue) tryString = TryConvertValues<string>(a, b);
      if (tryString.canConvert)
        return Compare(opValue, tryString.aValue, tryString.bValue);

      (bool canConvert, bool aValue, bool bValue) tryBool = TryConvertValues<bool>(a, b);
      if (tryBool.canConvert)
        return Compare(opValue, tryBool.aValue, tryBool.bValue);

      throw new ApplicationException("unexpected value type");
    }

    public override SyntaxNode Generate(Context context)
    {
      ExpressionSyntax firstExpression = Values.Generate("A", context) as ExpressionSyntax;
      if (firstExpression == null)
        throw new ApplicationException("Unknown expression for value A.");

      ExpressionSyntax secondExpression = Values.Generate("B", context) as ExpressionSyntax;
      if (secondExpression == null)
        throw new ApplicationException("Unknown expression for value B.");

      var opValue = Fields.Get("OP");

      var binaryOperator = GetBinaryOperator(opValue);
      var expression = BinaryExpression(binaryOperator, firstExpression, secondExpression);

      return ParenthesizedExpression(expression);
    }

    private bool Compare(string op, string a, string b)
    {
      switch (op)
      {
        case "EQ":
          return a == b;
        case "NEQ":
          return a != b;
        case "LT":
          return string.CompareOrdinal(a, b) < 0;
        case "LTE":
          return string.CompareOrdinal(a, b) <= 0;
        case "GT":
          return string.CompareOrdinal(a, b) > 0;
        case "GTE":
          return string.CompareOrdinal(a, b) >= 0;
        default:
          throw new ApplicationException($"Unknown OP {op}");
      }
    }

    private bool Compare(string op, double a, double b)
    {
      switch (op)
      {
        case "EQ":
          return a.Equals(b);
        case "NEQ":
          return !a.Equals(b);
        case "LT":
          return a < b;
        case "LTE":
          return a <= b;
        case "GT":
          return a > b;
        case "GTE":
          return a >= b;
        default:
          throw new ApplicationException($"Unknown OP {op}");
      }
    }

    private bool Compare(string op, bool a, bool b)
    {
      switch (op)
      {
        case "EQ":
          return a == b;
        case "NEQ":
          return a != b;
        case "LT":
          return Convert.ToByte(a) < Convert.ToByte(b);
        case "LTE":
          return Convert.ToByte(a) <= Convert.ToByte(b);
        case "GT":
          return Convert.ToByte(a) > Convert.ToByte(b);
        case "GTE":
          return Convert.ToByte(a) >= Convert.ToByte(b);
        default:
          throw new ApplicationException($"Unknown OP {op}");
      }
    }

    private SyntaxKind GetBinaryOperator(string op)
    {
      switch (op)
      {
        case "EQ":
          return SyntaxKind.EqualsExpression;
        case "NEQ":
          return SyntaxKind.NotEqualsExpression;
        case "LT":
          return SyntaxKind.LessThanExpression;
        case "LTE":
          return SyntaxKind.LessThanOrEqualExpression;
        case "GT":
          return SyntaxKind.GreaterThanExpression;
        case "GTE":
          return SyntaxKind.GreaterThanOrEqualExpression;
        default:
          throw new ApplicationException($"Unknown OP {op}");
      }
    }

    private (bool canConvert, T aValue, T bValue) TryConvertValues<T>(object a, object b)
    {
      T aResult;
      if (a?.GetType() == typeof(T))
        aResult = (T)Convert.ChangeType(a, typeof(T));
      else
        return (false, default, default);

      T bResult;
      if (b?.GetType() == typeof(T))
        bResult = (T)Convert.ChangeType(b, typeof(T));
      else
        return (false, default, default);

      return (true, aResult, bResult);
    }

    private (bool canConvert, double aValue, double bValue) TryConvertToDoubleValues(object a, object b)
    {
      double aResult;
      if (a?.GetType() == typeof(double) || a?.GetType() == typeof(int))
        aResult = (double)Convert.ChangeType(a, typeof(double));
      else
        return (false, default, default);

      double bResult;
      if (b?.GetType() == typeof(double) || b?.GetType() == typeof(int))
        bResult = (double)Convert.ChangeType(b, typeof(double));
      else
        return (false, default, default);

      return (true, aResult, bResult);
    }
  }
}