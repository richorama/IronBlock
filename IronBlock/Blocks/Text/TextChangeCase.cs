using System;
using System.Globalization;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
  public class TextCaseChange : IBlock
  {
    public override object Evaluate(Context context)
    {
      var toCase = this.Fields.Get("CASE").ToString();
      var text = (this.Values.Evaluate("TEXT", context) ?? "").ToString();

      switch (toCase)
      {
        case "UPPERCASE":
          return text.ToUpper();

        case "LOWERCASE":
          return text.ToLower();

        case "TITLECASE":
          {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(text);
          }

        default:
          throw new NotSupportedException("unknown case");

      }

    }

    public override SyntaxNode Generate(Context context)
    {
      var textExpression = this.Values.Generate("TEXT", context) as ExpressionSyntax;
      if (textExpression == null) throw new ApplicationException($"Unknown expression for text.");

      var toCase = this.Fields.Get("CASE");
      switch (toCase)
      {
        case "UPPERCASE": return SyntaxGenerator.MethodInvokeExpression(textExpression, nameof(string.ToUpper));
        case "LOWERCASE": return SyntaxGenerator.MethodInvokeExpression(textExpression, nameof(string.ToLower));
        case "TITLECASE":
          return SyntaxGenerator.MethodInvokeExpression(
MemberAccessExpression(
SyntaxKind.SimpleMemberAccessExpression,
SyntaxGenerator.PropertyAccessExpression(
IdentifierName(nameof(CultureInfo)),
nameof(CultureInfo.InvariantCulture)
),
IdentifierName(nameof(CultureInfo.TextInfo))
),
nameof(TextInfo.ToTitleCase),
textExpression);
        default: throw new NotSupportedException("unknown case");
      }
    }
  }
}