using System;
using System.Collections.Generic;
using System.Linq;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Lists
{
  public class ListsGetIndex : IBlock
  {
    static Random rnd = new Random();

    public override object Evaluate(Context context)
    {

      var values = this.Values.Evaluate("VALUE", context) as List<object>;
      var mode = this.Fields.Get("MODE");
      var where = this.Fields.Get("WHERE");

      var index = -1;
      switch (where)
      {
        case "FROM_START":
          index = Convert.ToInt32(this.Values.Evaluate("AT", context)) - 1;
          break;

        case "FROM_END":
          index = values.Count - Convert.ToInt32(this.Values.Evaluate("AT", context));
          break;

        case "FIRST":
          index = 0;
          break;

        case "LAST":
          index = values.Count - 1;
          break;

        case "RANDOM":
          index = rnd.Next(values.Count);
          break;

        default:
          throw new NotSupportedException($"unsupported where ({where})");
      }

      switch (mode)
      {
        case "GET":
          return values[index];

        case "GET_REMOVE":
          var value = values[index];
          values.RemoveAt(index);
          return value;

        case "REMOVE":
          values.RemoveAt(index);
          return null;

        default:
          throw new NotSupportedException($"unsupported mode ({mode})");
      }


    }

    public override SyntaxNode Generate(Context context)
    {
      var valueExpression = this.Values.Generate("VALUE", context) as ExpressionSyntax;
      if (valueExpression == null) throw new ApplicationException($"Unknown expression for value.");

      ExpressionSyntax atExpression = null;
      if (this.Values.Any(x => x.Name == "AT"))
      {
        atExpression = this.Values.Generate("AT", context) as ExpressionSyntax;
      }

      var mode = this.Fields.Get("MODE");
      switch (mode)
      {
        case "GET":
          break;
        case "GET_REMOVE":
        case "REMOVE":
        default: throw new NotSupportedException($"unknown mode {mode}");
      }

      var where = this.Fields.Get("WHERE");
      switch (where)
      {
        case "FROM_START":
          if (atExpression == null) throw new ApplicationException($"Unknown expression for at.");

          return
              ElementAccessExpression(
                  valueExpression
              )
              .WithArgumentList(
                  BracketedArgumentList(
                      SingletonSeparatedList(
                          Argument(
                              BinaryExpression(
                                  SyntaxKind.SubtractExpression,
                                  atExpression,
                                  LiteralExpression(
                                      SyntaxKind.NumericLiteralExpression,
                                      Literal(1)
                                  )
                              )
                          )
                      )
                  )
              );
        case "FROM_END":
          if (atExpression == null) throw new ApplicationException($"Unknown expression for at.");

          return
              SyntaxGenerator.MethodInvokeExpression(
                  SyntaxGenerator.MethodInvokeExpression(
                      valueExpression,
                      "TakeLast",
                      atExpression),
                  nameof(Enumerable.First)
              ); ;
        case "FIRST":
          return SyntaxGenerator.MethodInvokeExpression(valueExpression, nameof(Enumerable.First));
        case "LAST":
          return SyntaxGenerator.MethodInvokeExpression(valueExpression, nameof(Enumerable.Last));
        case "RANDOM":
        default:
          throw new NotSupportedException($"unknown where {where}");
      }
    }
  }
}