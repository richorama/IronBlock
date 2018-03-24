using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Math
{
    public class MathConstrain : IBlock
    {
        public override object Evaluate(Context context)
        {
            var value = (double) this.Values.Evaluate("VALUE", context);
            var low = (double) this.Values.Evaluate("LOW", context);
            var high = (double) this.Values.Evaluate("HIGH", context);

            return System.Math.Min(System.Math.Max(value, low), high);
        }

		public override SyntaxNode Generate(Context context)
		{
			var valueExpression = this.Values.Generate("VALUE", context) as ExpressionSyntax;
			if (valueExpression == null) throw new ApplicationException($"Unknown expression for value.");

			var lowExpression = this.Values.Generate("LOW", context) as ExpressionSyntax;
			if (lowExpression == null) throw new ApplicationException($"Unknown expression for low.");

			var highExpression = this.Values.Generate("HIGH", context) as ExpressionSyntax;
			if (highExpression == null) throw new ApplicationException($"Unknown expression for high.");

			return
				InvocationExpression(
					MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						IdentifierName(nameof(System.Math)),
						IdentifierName(nameof(System.Math.Min))
					)
				)
				.WithArgumentList(
					ArgumentList(
						SeparatedList<ArgumentSyntax>(
							new SyntaxNodeOrToken[]{
								Argument(
									InvocationExpression(
										MemberAccessExpression(
											SyntaxKind.SimpleMemberAccessExpression,
											IdentifierName(nameof(System.Math)),
											IdentifierName(nameof(System.Math.Max))
										)
									)
									.WithArgumentList(
										ArgumentList(
											SeparatedList<ArgumentSyntax>(
												new SyntaxNodeOrToken[]{
													Argument(valueExpression),
													Token(SyntaxKind.CommaToken),
													Argument(lowExpression)
												}
											)
										)
									)
								),
								Token(SyntaxKind.CommaToken),
								Argument(highExpression)
							}
						)
					)
				);
		}
	}

}