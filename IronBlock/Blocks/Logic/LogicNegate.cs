using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicNegate : IBlock
    {
        public override object Evaluate(Context context)
        {
            return !((bool) (this.Values.Evaluate("BOOL", context) ?? false));
        }

    }

}