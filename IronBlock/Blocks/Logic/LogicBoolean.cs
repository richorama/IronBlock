using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicBoolean : IBlock
    {
        public override object Evaluate(Context context)
        {
            return bool.Parse(this.Fields.Get("BOOL"));
        }

    }

}