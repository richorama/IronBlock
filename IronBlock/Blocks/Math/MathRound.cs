using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathRound : IBlock
    {
        public override object Evaluate(Context context)
        {
            var op = this.Fields.Get("OP");
            var number = (double) this.Values.Evaluate("NUM", context);

            switch (op)
            {
                case "ROUND": return System.Math.Round(number);            
                case "ROUNDUP": return System.Math.Ceiling(number);            
                case "ROUNDDOWN": return System.Math.Floor(number);            
                default: throw new ApplicationException($"Unknown OP {op}");
            }
        }

    }

}