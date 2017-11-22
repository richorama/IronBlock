using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicOperation : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var a = (bool) (this.Values.Evaluate("A", variables) ?? false);
            var b = (bool) (this.Values.Evaluate("B", variables) ?? false);
            
            var op = this.Fields.Evaluate("OP");

            switch (op)
            {
                case "AND": return a && b;
                case "OR": return a || b;
                default: throw new ApplicationException($"Unknown OP {op}");
            }

        }

    }

}