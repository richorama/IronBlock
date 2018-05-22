using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Lists
{
	public class ListsSetIndex : IBlock
	{
		public override SyntaxNode Generate(Context context)
		{
			var listExpression = this.Values.Generate("LIST", context) as ExpressionSyntax;
			if (listExpression == null) throw new ApplicationException($"Unknown expression for list.");

			var toExpression = this.Values.Generate("TO", context) as ExpressionSyntax;
			if (toExpression == null) throw new ApplicationException($"Unknown expression for to.");

			ExpressionSyntax atExpression = null;
			if (this.Values.Any(x => x.Name == "AT"))
			{
				atExpression = this.Values.Generate("AT", context) as ExpressionSyntax;
			}

			var mode = this.Fields.Get("MODE");
			switch (mode)
			{
				case "SET":
					break;
				case "INSERT_AT": 
				default: throw new NotSupportedException($"unknown mode {mode}");
			}

			var where = this.Fields.Get("WHERE");
			switch (where)
			{
				case "FROM_START":
					if (atExpression == null) throw new ApplicationException($"Unknown expression for at.");

					return
						AssignmentExpression(
							SyntaxKind.SimpleAssignmentExpression,
							ElementAccessExpression(
								listExpression
							)
							.WithArgumentList(
								BracketedArgumentList(
									SingletonSeparatedList(
										Argument(
											BinaryExpression(
												SyntaxKind.SubtractExpression,
												atExpression,
												LiteralExpression(
													SyntaxKind.NumericLiteralExpression,
													Literal(1)
												)
											)
										)
									)
								)
							),
							toExpression
						);

				case "FROM_END":
					return
						AssignmentExpression(
							SyntaxKind.SimpleAssignmentExpression,
							ElementAccessExpression(
								listExpression
							)
							.WithArgumentList(
								BracketedArgumentList(
									SingletonSeparatedList(
										Argument(
											BinaryExpression(
												SyntaxKind.SubtractExpression,
												MemberAccessExpression(
													SyntaxKind.SimpleMemberAccessExpression,
													listExpression,
													IdentifierName("Count")
												),
												atExpression
											)
										)
									)
								)
							),
							toExpression
						);

				case "FIRST":
					return
						AssignmentExpression(
							SyntaxKind.SimpleAssignmentExpression,
							ElementAccessExpression(
								listExpression
							)
							.WithArgumentList(
								BracketedArgumentList(
									SingletonSeparatedList(
										Argument(											
											LiteralExpression(
												SyntaxKind.NumericLiteralExpression,
												Literal(0)
											)
										)
									)
								)
							),
							toExpression
						);
					
				case "LAST":
					return 
						AssignmentExpression(
							SyntaxKind.SimpleAssignmentExpression,
							ElementAccessExpression(
								listExpression
							)
							.WithArgumentList(
								BracketedArgumentList(
									SingletonSeparatedList(
										Argument(
											BinaryExpression(
												SyntaxKind.SubtractExpression,
												MemberAccessExpression(
													SyntaxKind.SimpleMemberAccessExpression,
													listExpression,
													IdentifierName("Count")
												),
												LiteralExpression(
													SyntaxKind.NumericLiteralExpression,
													Literal(1)
												)
											)
										)
									)
								)
							),
							toExpression
						);

				case "RANDOM":
				default:
					throw new NotSupportedException($"unknown where {where}");
			}
		}
	}
}