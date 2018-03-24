using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Lists
{
	public class ListsRepeat : IBlock
	{
		public override object Evaluate(Context context)
		{
			var item = this.Values.Evaluate("ITEM", context);
			var num = (double)this.Values.Evaluate("NUM", context);

			var list = new List<object>();
			for (var i = 0; i < num; i++)
			{
				list.Add(item);

			}
			return list;

		}

		public override SyntaxNode Generate(Context context)
		{
			var itemExpression = this.Values.Generate("ITEM", context) as ExpressionSyntax;
			if (itemExpression == null) throw new ApplicationException($"Unknown expression for item.");

			var numExpression = this.Values.Generate("NUM", context) as ExpressionSyntax;
			if (numExpression == null) throw new ApplicationException($"Unknown expression for number.");

			return
				InvocationExpression(
					MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						IdentifierName(nameof(Enumerable)),
						IdentifierName(nameof(Enumerable.Repeat))
					)
				)
				.WithArgumentList(
					ArgumentList(
						SeparatedList<ArgumentSyntax>(
							new SyntaxNodeOrToken[]{
								Argument(itemExpression),
								Token(SyntaxKind.CommaToken),
								Argument(numExpression)
							}
						)
					)
				);
		}
	}
}