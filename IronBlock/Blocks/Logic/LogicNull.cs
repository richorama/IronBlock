using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicNull : IBlock
    {
        public override object Evaluate(Context context)
        {
            return null;
        }

    }

}