using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Controls
{
  public class ControlsFor : IBlock
  {
    public override object Evaluate(Context context)
    {
      var variableName = this.Fields.Get("VAR");

      var fromValue = (double)this.Values.Evaluate("FROM", context);
      var toValue = (double)this.Values.Evaluate("TO", context);
      var byValue = (double)this.Values.Evaluate("BY", context);

      var statement = this.Statements.FirstOrDefault();


      if (context.Variables.ContainsKey(variableName))
      {
        context.Variables[variableName] = fromValue;
      }
      else
      {
        context.Variables.Add(variableName, fromValue);
      }


      while ((double)context.Variables[variableName] <= toValue)
      {
        statement.Evaluate(context);
        context.Variables[variableName] = (double)context.Variables[variableName] + byValue;
      }

      return base.Evaluate(context);
    }

    public override SyntaxNode Generate(Context context)
    {
      var variableName = this.Fields.Get("VAR").CreateValidName();

      var fromValueExpression = this.Values.Generate("FROM", context) as ExpressionSyntax;
      if (fromValueExpression == null) throw new ApplicationException($"Unknown expression for from value.");

      var toValueExpression = this.Values.Generate("TO", context) as ExpressionSyntax;
      if (toValueExpression == null) throw new ApplicationException($"Unknown expression for to value.");

      var byValueExpression = this.Values.Generate("BY", context) as ExpressionSyntax;
      if (byValueExpression == null) throw new ApplicationException($"Unknown expression for by value.");

      var statement = this.Statements.FirstOrDefault();

      var rootContext = context.GetRootContext();
      if (!rootContext.Variables.ContainsKey(variableName))
      {
        rootContext.Variables[variableName] = null;
      }

      var forContext = new Context() { Parent = context };
      if (statement?.Block != null)
      {
        var statementSyntax = statement.Block.GenerateStatement(forContext);
        if (statementSyntax != null)
        {
          forContext.Statements.Add(statementSyntax);
        }
      }

      var forStatement =
          ForStatement(
                Block(forContext.Statements)
              )
              .WithInitializers(
                SingletonSeparatedList<ExpressionSyntax>(
                  AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    IdentifierName(variableName),
                    fromValueExpression
                  )
                )
              )
              .WithCondition(
                BinaryExpression(
                  SyntaxKind.LessThanOrEqualExpression,
                  IdentifierName(variableName),
                  toValueExpression
                )
              )
              .WithIncrementors(
                SingletonSeparatedList<ExpressionSyntax>(
                  AssignmentExpression(
                    SyntaxKind.AddAssignmentExpression,
                    IdentifierName(variableName),
                    byValueExpression
                  )
                )
              );

      return Statement(forStatement, base.Generate(context), context);
    }
  }

}