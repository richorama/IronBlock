using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathModulo: IBlock
    {
        public override object Evaluate(Context context)
        {
            var dividend = (double) this.Values.Evaluate("DIVIDEND", context);
            var divisor = (double) this.Values.Evaluate("DIVISOR", context);

            return dividend % divisor;
        }

    }

}