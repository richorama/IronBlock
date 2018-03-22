using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
    public class TextPrint : IBlock
    {
        public override object Evaluate(Context context)
        {
            var text = this.Values.Evaluate("TEXT", context);

            Console.WriteLine(text);

            return base.Evaluate(context);
        }

		public override SyntaxNode Generate(Context context)
		{
			SyntaxNode syntaxNode = this.Values.Generate("TEXT", context);
			var expression = syntaxNode as ExpressionSyntax;
			if (expression == null) throw new ApplicationException($"Unknown expression for text.");

			var invocationExpression =
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
					);			

			var next = base.Generate(context);
			if (next == null)
				return invocationExpression;

			context.Statements.Insert(0, ExpressionStatement(invocationExpression));
			return next;
		}
	}

}