using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathNumber : IBlock
    {
        public override object Evaluate(Context context)
        {
            return double.Parse(this.Fields.Get("NUM"));
        }
    }

}