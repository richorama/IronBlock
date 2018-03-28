using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Lists
{
	public class ListsGetIndex : IBlock
	{
		public override object Evaluate(Context context)
		{
			throw new NotImplementedException();
		}

		public override SyntaxNode Generate(Context context)
		{
			var valueExpression = this.Values.Generate("VALUE", context) as ExpressionSyntax;
			if (valueExpression == null) throw new ApplicationException($"Unknown expression for value.");

			ExpressionSyntax atExpression = null;
			if (this.Values.Any(x => x.Name == "AT"))
			{
				atExpression = this.Values.Generate("AT", context) as ExpressionSyntax;
			}
			
			var mode = this.Fields.Get("MODE");
			switch (mode)
			{
				case "GET":
					break;
				case "GET_REMOVE": 
				case "REMOVE":
				default: throw new NotSupportedException($"unknown mode {mode}");
			}

			var where = this.Fields.Get("WHERE");
			switch (where)
			{
				case "FROM_START":
					if (atExpression == null) throw new ApplicationException($"Unknown expression for at.");

					return 
						ElementAccessExpression(
							valueExpression
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
						);
				case "FROM_END":
					if (atExpression == null) throw new ApplicationException($"Unknown expression for at.");

					return
						SyntaxGenerator.MethodInvokeExpression(
							SyntaxGenerator.MethodInvokeExpression(
								valueExpression, 
								"TakeLast", 
								atExpression), 
							nameof(Enumerable.First)
						);;
				case "FIRST":
					return SyntaxGenerator.MethodInvokeExpression(valueExpression, nameof(Enumerable.First));
				case "LAST":
					return SyntaxGenerator.MethodInvokeExpression(valueExpression, nameof(Enumerable.Last));
				case "RANDOM":
				default:
					throw new NotSupportedException($"unknown where {where}");
			}
		}
	}
}