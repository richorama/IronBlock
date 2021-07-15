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
  public class ListsLength : IBlock
  {
    public override object Evaluate(Context context)
    {
      var value = this.Values.Evaluate("VALUE", context) as IEnumerable<object>;
      if (null == value) return 0.0;

      return (double)value.Count();
    }
    public override SyntaxNode Generate(Context context)
    {
      var valueExpression = this.Values.Generate("VALUE", context) as ExpressionSyntax;
      if (valueExpression == null) throw new ApplicationException($"Unknown expression for value.");

      return SyntaxGenerator.PropertyAccessExpression(valueExpression, nameof(Array.Length));
    }
  }
}