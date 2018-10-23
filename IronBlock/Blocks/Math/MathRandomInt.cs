using System;
using System.Collections.Generic;
using System.Linq;
using IronBlock.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Math
{
	public class MathRandomInt : IBlock
	{
		static Random rand = new Random();

		public override object Evaluate(Context context)
		{
			var from = (double) this.Values.Evaluate("FROM", context);
			var to = (double) this.Values.Evaluate("TO", context);
			//return 2.2;
			return (double) rand.Next((int)System.Math.Min(from, to), (int)System.Math.Max(from, to));
		}

	}
}