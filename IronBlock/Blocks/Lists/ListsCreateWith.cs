using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Lists
{
  public class ListsCreateWith : IBlock
  {
    public override object Evaluate(Context context)
    {
      var list = new List<object>();
      foreach (var value in this.Values)
      {
        list.Add(value.Evaluate(context));

      }
      return list;

    }

    public override SyntaxNode Generate(Context context)
    {
      var expressions = new List<ExpressionSyntax>();

      foreach (var value in this.Values)
      {
        var itemExpression = value.Generate(context) as ExpressionSyntax;
        if (itemExpression == null) throw new ApplicationException($"Unknown expression for item.");

        expressions.Add(itemExpression);
      }

      return
          ObjectCreationExpression(
              GenericName(
                  Identifier(nameof(List))
              )
              .WithTypeArgumentList(
                  TypeArgumentList(
                      SingletonSeparatedList<TypeSyntax>(
                          IdentifierName("dynamic")
                      )
                  )
              )
          )
          .WithInitializer(
              InitializerExpression(
                  SyntaxKind.CollectionInitializerExpression,
                  SeparatedList(expressions)
              )
          );
    }
  }
}