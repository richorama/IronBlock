using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
  public class TextAppend : IBlock
  {
    public override object Evaluate(Context context)
    {
      var variables = context.Variables;

      var variableName = this.Fields.Get("VAR");
      var textToAppend = (this.Values.Evaluate("TEXT", context) ?? "").ToString();

      if (!variables.ContainsKey(variableName))
      {
        variables.Add(variableName, "");
      }
      var value = variables[variableName].ToString();

      variables[variableName] = value + textToAppend;

      return base.Evaluate(context);
    }

    public override SyntaxNode Generate(Context context)
    {
      var variables = context.Variables;
      var variableName = this.Fields.Get("VAR").CreateValidName();

      var textExpression = this.Values.Generate("TEXT", context) as ExpressionSyntax;
      if (textExpression == null)
        throw new ApplicationException($"Unknown expression for text.");

      context.GetRootContext().Variables[variableName] = textExpression;

      var assignment =
        AssignmentExpression(
          SyntaxKind.AddAssignmentExpression,
          IdentifierName(variableName),
          textExpression
        );

      return Statement(assignment, base.Generate(context), context);
    }
  }
}