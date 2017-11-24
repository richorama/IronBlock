using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathSingle : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var op = this.Fields.Evaluate("OP");
            var number = (double) this.Values.Evaluate("NUM", variables);

            switch (op)
            {
                case "ROOT": return System.Math.Sqrt(number);
                case "SIN": return System.Math.Sin(number / 180 * System.Math.PI);
                default: throw new ApplicationException($"Unknown OP {op}");
            }
        }
    }

}