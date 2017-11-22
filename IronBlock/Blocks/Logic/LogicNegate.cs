using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicNegate : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            return !((bool) this.Values.Evaluate("BOOL", variables));
        }

    }

}