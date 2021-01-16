using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
  public class TextJoin : IBlock
  {
    public override object Evaluate(Context context)
    {
      var items = int.Parse(this.Mutations.GetValue("items"));

      var sb = new StringBuilder();
      for (var i = 0; i < items; i++)
      {
        if (!this.Values.Any(x => x.Name == $"ADD{i}")) continue;
        sb.Append(this.Values.Evaluate($"ADD{i}", context));
      }

      return sb.ToString();
    }

    public override SyntaxNode Generate(Context context)
    {
      var items = int.Parse(this.Mutations.GetValue("items"));

      var arguments = new List<ExpressionSyntax>();

      for (var i = 0; i < items; i++)
      {
        if (!this.Values.Any(x => x.Name == $"ADD{i}")) continue;
        var addExpression = this.Values.Generate($"ADD{i}", context) as ExpressionSyntax;
        if (addExpression == null) throw new ApplicationException($"Unknown expression for ADD{i}.");

        arguments.Add(addExpression);
      }

      if (!arguments.Any())
        return base.Generate(context);

      return
        SyntaxGenerator.MethodInvokeExpression(
          PredefinedType(
            Token(SyntaxKind.StringKeyword)
          ),
          nameof(string.Concat),
          arguments
        );
    }
  }
}