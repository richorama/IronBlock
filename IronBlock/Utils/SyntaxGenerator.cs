using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Utils
{
	public static class SyntaxGenerator
	{
		public static MemberAccessExpressionSyntax PropertyAccessExpression(ExpressionSyntax targetExpression, string propertyName)
		{
			return 
				MemberAccessExpression(
					SyntaxKind.SimpleMemberAccessExpression,
					targetExpression,
					IdentifierName(propertyName)
				);
		}

		public static InvocationExpressionSyntax MethodInvokeExpression(ExpressionSyntax targetExpression, string methodName, ExpressionSyntax argumentExpression)
		{
			return MethodInvokeExpression(targetExpression, methodName, new[] { argumentExpression });
		}

		public static InvocationExpressionSyntax MethodInvokeExpression(ExpressionSyntax targetExpression, string methodName, IEnumerable<ExpressionSyntax> argumentExpressions = null)
		{
			var invocationExpression =
				InvocationExpression(
					MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						targetExpression,
						IdentifierName(methodName)
					)
				);

			var arguments = argumentExpressions?.Select(x => Argument(x));
			if (arguments?.Any() ?? false)
			{
				invocationExpression =
					invocationExpression
						.WithArgumentList(
							ArgumentList(
								SeparatedList(arguments)
							)
						);
			}

			return invocationExpression;
		}
	}
}
