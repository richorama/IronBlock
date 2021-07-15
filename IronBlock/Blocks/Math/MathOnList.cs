using System;
using System.Collections.Generic;
using System.Linq;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IronBlock.Blocks.Math
{
  public class MathOnList : IBlock
  {
    static Random rnd = new Random();
    public override object Evaluate(Context context)
    {
      var op = this.Fields.Get("OP");
      var list = this.Values.Evaluate("LIST", context) as IEnumerable<object>;

      var doubleList = list.Select(x => (double)x).ToArray();

      switch (op)
      {
        case "SUM": return doubleList.Sum();
        case "MIN": return doubleList.Min();
        case "MAX": return doubleList.Max();
        case "AVERAGE": return doubleList.Average();
        case "MEDIAN": return Median(doubleList);
        case "RANDOM": return doubleList.Any() ? doubleList[rnd.Next(doubleList.Count())] as object : null;
        case "MODE": return doubleList.Any() ? doubleList.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key as object : null;

        case "STD_DEV":
          throw new NotImplementedException($"OP {op} not implemented");

        default: throw new ApplicationException($"Unknown OP {op}");
      }
    }

    public override SyntaxNode Generate(Context context)
    {
      var listExpression = this.Values.Generate("LIST", context) as ExpressionSyntax;
      if (listExpression == null) throw new ApplicationException($"Unknown expression for list.");

      var op = this.Fields.Get("OP");

      switch (op)
      {
        case "SUM":
          return SyntaxGenerator.MethodInvokeExpression(listExpression, "Sum");
        case "MIN":
          return SyntaxGenerator.MethodInvokeExpression(listExpression, "Min");
        case "MAX":
          return SyntaxGenerator.MethodInvokeExpression(listExpression, "Max");
        case "AVERAGE":
          return SyntaxGenerator.MethodInvokeExpression(listExpression, "Average");
        case "MEDIAN":
        case "RANDOM":
        case "MODE":
        case "STD_DEV":
          throw new NotImplementedException($"OP {op} not implemented");

        default: throw new ApplicationException($"Unknown OP {op}");
      }
    }

    object Median(IEnumerable<double> values)
    {
      if (!values.Any()) return null;
      var sortedValues = values.OrderBy(x => x).ToArray();
      double mid = (sortedValues.Length - 1) / 2.0;
      return (sortedValues[(int)(mid)] + sortedValues[(int)(mid + 0.5)]) / 2;
    }
  }
}