using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicOperation : IBlock
    {
        public override object Evaluate(Context context)
        {
            var a = (bool) (this.Values.Evaluate("A", context) ?? false);
            var b = (bool) (this.Values.Evaluate("B", context) ?? false);
            
            var op = this.Fields.Get("OP");

            switch (op)
            {
                case "AND": return a && b;
                case "OR": return a || b;
                default: throw new ApplicationException($"Unknown OP {op}");
            }

        }

    }

}