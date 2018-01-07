using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathConstant : IBlock
    {
        public override object Evaluate(Context context)
        {
            var constant = this.Fields.Get("CONSTANT");

            switch (constant)
            {
                case "PI": return System.Math.PI;
                case "E": return System.Math.E;
                case "GOLDEN_RATIO": return (1 + System.Math.Sqrt(5)) / 2;
                case "SQRT2" : return System.Math.Sqrt(2);
                case "SQRT1_2": return System.Math.Sqrt(0.5);
                case "INFINITY": return double.PositiveInfinity;
                default: throw new ApplicationException($"Unknown CONSTANT {constant}");
            }
        }
    }

}