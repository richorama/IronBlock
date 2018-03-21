using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
    public class TextPrompt : IBlock
    {
        public override object Evaluate(Context context)
        {
            var inputType = this.Mutations.GetValue("type") ?? "TEXT";

            var text = (this.Values.Evaluate("TEXT", context) ?? "").ToString();

            if (!string.IsNullOrWhiteSpace(text))
            {
                Console.Write($"{text}: ");
            }

            var value = Console.ReadLine();

            if (inputType == "NUMBER")
            {
                return int.Parse(value);                
            }

            return value;
        }

		public override SyntaxNode Generate(Context context)
		{
			var inputType = this.Mutations.GetValue("type") ?? "TEXT";

			var expression = this.Values.Generate("TEXT", context) as ExpressionSyntax;
			if (expression != null)
			{
				context.Statements.Add(
					ExpressionStatement(
						InvocationExpression(
						MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								IdentifierName("Console"),
								IdentifierName("WriteLine")
							)
						)
						.WithArgumentList(
							ArgumentList(
								SingletonSeparatedList(
									Argument(
										expression
									)
								)
							)
						)
					)
				);
			}

			context.Statements.Add(
				LocalDeclarationStatement(
					VariableDeclaration(
						IdentifierName("var")
					)
					.WithVariables(
						SingletonSeparatedList<VariableDeclaratorSyntax>(
							VariableDeclarator(
								Identifier("value")
							)
							.WithInitializer(
								EqualsValueClause(
									InvocationExpression(
										MemberAccessExpression(
											SyntaxKind.SimpleMemberAccessExpression,
											IdentifierName("Console"),
											IdentifierName("ReadLine")
										)
									)
								)
							)
						)
					)
				)
			);

			if (inputType == "NUMBER")
			{
				return
					InvocationExpression(
						MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							PredefinedType(
								Token(SyntaxKind.IntKeyword)),
							IdentifierName("Parse")
						)
					)
					.WithArgumentList(
						ArgumentList(
							SingletonSeparatedList(
								Argument(
									IdentifierName("value")
								)
							)
						)
					);
			}

			return IdentifierName("value");
		}
	}

}