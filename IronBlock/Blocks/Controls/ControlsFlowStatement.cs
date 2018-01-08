using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Controls
{
    public class ControlsFlowStatement : IBlock
    {
        public override object Evaluate(Context context)
        {
            var flow = this.Fields.Get("FLOW");
            if (flow == "CONTINUE")
            {
                context.EscapeMode = EscapeMode.Continue;
                return null;
            }


            if (flow == "BREAK")
            {
                context.EscapeMode = EscapeMode.Break;
                return null;
            }

            throw new NotSupportedException($"{flow} flow is not supported");
        }
    }

}