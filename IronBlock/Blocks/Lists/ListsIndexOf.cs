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
  public class ListsIndexOf : IBlock
  {
    public override object Evaluate(Context context)
    {
      var direction = this.Fields.Get("END");
      var value = this.Values.Evaluate("VALUE", context) as IEnumerable<object>;
      var find = this.Values.Evaluate("FIND", context);

      switch (direction)
      {
        case "FIRST":
          return value.ToList().IndexOf(find) + 1;

        case "LAST":
          return value.ToList().LastIndexOf(find) + 1;

        default:
          throw new NotSupportedException("$Unknown end: {direction}");
      }
    }

    public override SyntaxNode Generate(Context context)
    {
      throw new NotImplementedException();
    }
  }
}