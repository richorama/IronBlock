using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Logic
{
    public class LogicTernary : IBlock
    {
        public override object Evaluate(Context context)
        {
            var ifValue = (bool) this.Values.Evaluate("IF", context);
            
            if (ifValue)
            {
                if (this.Values.Any(x => x.Name == "THEN"))
                {
                    return this.Values.Evaluate("THEN", context);
                }
            }
            else
            {
                if (this.Values.Any(x => x.Name == "ELSE"))
                {
                    return this.Values.Evaluate("ELSE", context);
                }
            }
            return null;
        }

    }

}