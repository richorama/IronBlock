using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Math
{
    public class MathArithmetic : IBlock
    {
        public override object Evaluate(Context context)
        {
            var a = (double) this.Values.Evaluate("A", context);
            var b = (double) this.Values.Evaluate("B", context);
            
            var opValue = this.Fields.Get("OP");

            switch (opValue)
            {
                case "MULTIPLY": return a * b;
                case "DIVIDE": return a / b;
                case "ADD": return a + b;
                case "SUBTRACT": return a - b;

                default: throw new ApplicationException($"Unknown OP {opValue}");
            }
        }

		public override SyntaxNode Generate(Context context)
		{
			var firstExpression = this.Values.Generate("A", context) as ExpressionSyntax;
			if (firstExpression == null)
				throw new ApplicationException($"Unknown expression for value A.");

			var secondExpression = this.Values.Generate("B", context) as ExpressionSyntax;
			if (secondExpression == null)
				throw new ApplicationException($"Unknown expression for value B.");

			var opValue = this.Fields.Get("OP");
			var binaryOperator = GetBinaryOperator(opValue);
			return ParenthesizedExpression(BinaryExpression(binaryOperator, firstExpression, secondExpression));
		}

		private SyntaxKind GetBinaryOperator(string opValue)
		{
			switch (opValue)
			{
				case "MULTIPLY": return SyntaxKind.MultiplyExpression;
				case "DIVIDE": return SyntaxKind.DivideExpression;
				case "ADD": return SyntaxKind.AddExpression;
				case "SUBTRACT": return SyntaxKind.SubtractExpression;

				default: throw new ApplicationException($"Unknown OP {opValue}");
			}
		}
	}

}