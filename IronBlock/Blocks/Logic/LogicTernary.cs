using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Logic
{
    public class LogicTernary : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var ifValue = (bool) this.Values.Evaluate("IF", variables);
            
            if (ifValue)
            {
                if (this.Values.Any(x => x.Name == "THEN"))
                {
                    return this.Values.Evaluate("THEN", variables);
                }
            }
            else
            {
                if (this.Values.Any(x => x.Name == "ELSE"))
                {
                    return this.Values.Evaluate("ELSE", variables);
                }
            }
            return null;
        }

    }

}