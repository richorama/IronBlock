using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Lists
{
    public class ListsSplit : IBlock
    {
        public override object Evaluate(Context context)
        {
            var mode = this.Fields.Get("MODE");
            var input = this.Values.Evaluate("INPUT", context);
            var delim = this.Values.Evaluate("DELIM", context);

            switch (mode)
            {
                case "SPLIT":
                    return input
                        .ToString()
                        .Split(new string[] {delim.ToString() }, StringSplitOptions.None)
                        .ToList();

                case "JOIN":
                    return string
                        .Join(delim.ToString(), (input as IEnumerable<object>).Select(x => x.ToString()));

                default:
                    throw new NotSupportedException($"unknown mode: {mode}");

            }
        }

		public override SyntaxNode Generate(Context context)
		{
			var mode = this.Fields.Get("MODE");
			var inputExpression = this.Values.Generate("INPUT", context) as ExpressionSyntax;
			if (inputExpression == null) throw new ApplicationException($"Unknown expression for input.");

			var delimExpression = this.Values.Generate("DELIM", context) as ExpressionSyntax;
			if (delimExpression == null) throw new ApplicationException($"Unknown expression for delim.");

			switch (mode)
			{
				case "SPLIT":
					return
						InvocationExpression(
							MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								InvocationExpression(
									MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										inputExpression,
										IdentifierName(nameof(object.ToString))
									)
								),
								IdentifierName(nameof(string.Split))
							)
						)
						.WithArgumentList(
							ArgumentList(
								SingletonSeparatedList(
									Argument(
										delimExpression
									)
								)
							)
						);

				case "JOIN":
					return
						InvocationExpression(
							MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								PredefinedType(
									Token(SyntaxKind.StringKeyword)
								),
								IdentifierName(nameof(string.Join))
							)
						)
						.WithArgumentList(
							ArgumentList(
								SeparatedList<ArgumentSyntax>(
									new SyntaxNodeOrToken[]{
										Argument(delimExpression),
										Token(SyntaxKind.CommaToken),
										Argument(inputExpression)
									}
								)
							)
						);

				default:
					throw new NotSupportedException($"unknown mode: {mode}");
			}
		}
	}
}