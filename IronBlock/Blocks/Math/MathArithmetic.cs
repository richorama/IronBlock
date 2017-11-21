using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathArithmetic : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var a = (double) this.Values.Evaluate("A", variables);
            var b = (double) this.Values.Evaluate("B", variables);
            
            var opValue = this.Fields.Evaluate("OP");

            switch (opValue)
            {
                case "MULTIPLY": return a * b;
                case "DIVIDE": return a / b;
                case "ADD": return a + b;
                case "SUBTRACT": return a - b;

                default: throw new ApplicationException($"Unknown OP {opValue}");
            }
        }
    }

}