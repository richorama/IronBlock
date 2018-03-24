using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Math
{
	public class MathConstant : IBlock
	{
		public override object Evaluate(Context context)
		{
			var constant = this.Fields.Get("CONSTANT");
			return GetValue(constant);
		}

		static double GetValue(string constant)
		{
			switch (constant)
			{
				case "PI": return System.Math.PI;
				case "E": return System.Math.E;
				case "GOLDEN_RATIO": return (1 + System.Math.Sqrt(5)) / 2;
				case "SQRT2": return System.Math.Sqrt(2);
				case "SQRT1_2": return System.Math.Sqrt(0.5);
				case "INFINITY": return double.PositiveInfinity;
				default: throw new ApplicationException($"Unknown CONSTANT {constant}");
			}
		}

		public override SyntaxNode Generate(Context context)
		{
			var constant = this.Fields.Get("CONSTANT");
			
			switch (constant)
			{
				case "PI":
					return MathConstantOf(nameof(System.Math.PI));
				case "E":
					return MathConstantOf(nameof(System.Math.E));
				case "SQRT2":
					return SqrtOf(2);
				case "SQRT1_2":
					return SqrtOf(0.5);
				case "INFINITY":
					return MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						PredefinedType(
							Token(SyntaxKind.DoubleKeyword)
						),
						IdentifierName(nameof(double.PositiveInfinity))
					);
				default:
					{
						var value = GetValue(constant);
						return LiteralExpression(
							SyntaxKind.NumericLiteralExpression,
							Literal(value));
					}
			}
		}

		private SyntaxNode MathConstantOf(string constant)
		{
			return MemberAccessExpression(
				SyntaxKind.SimpleMemberAccessExpression,
				IdentifierName(nameof(System.Math)),
				IdentifierName(constant)
			);
		}

		private SyntaxNode SqrtOf(double value)
		{
			return InvocationExpression(
				MemberAccessExpression(
					SyntaxKind.SimpleMemberAccessExpression,
					IdentifierName(nameof(System.Math)),
					IdentifierName(nameof(System.Math.Sqrt))
				)
			)
			.WithArgumentList(
				ArgumentList(
					SingletonSeparatedList(
						Argument(
							LiteralExpression(
								SyntaxKind.NumericLiteralExpression,
								Literal(value)
							)
						)
					)
				)
			);
		}
	}

}