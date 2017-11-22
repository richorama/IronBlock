using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicBoolean : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            return bool.Parse(this.Fields.Evaluate("BOOL"));
        }

    }

}