using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicNull : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            return null;
        }

    }

}