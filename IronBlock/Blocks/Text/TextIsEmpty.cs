using System;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
  public class TextIsEmpty : IBlock
  {
    public override object Evaluate(Context context)
    {
      var text = (this.Values.Evaluate("VALUE", context) ?? "").ToString();

      return string.IsNullOrEmpty(text);
    }

    public override SyntaxNode Generate(Context context)
    {
      var textExpression = this.Values.Generate("VALUE", context) as ExpressionSyntax;
      if (textExpression == null) throw new ApplicationException($"Unknown expression for text.");
      return SyntaxGenerator.MethodInvokeExpression(PredefinedType(Token(SyntaxKind.StringKeyword)), nameof(string.IsNullOrEmpty), textExpression);
    }
  }
}