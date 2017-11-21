using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathNumber : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            return double.Parse(this.Fields.Evaluate("NUM"));
        }
    }

}