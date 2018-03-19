using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
    }

}