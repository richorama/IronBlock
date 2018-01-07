using System;
using System.Collections.Generic;

namespace IronBlock.Blocks.Logic
{
    public class LogicCompare : IBlock
    {
        public override object Evaluate(Context context)
        {
            var a = this.Values.Evaluate("A", context);
            var b = this.Values.Evaluate("B", context);
            
            var opValue = this.Fields.Get("OP");

            if (a is string) return Compare(opValue, a as string, b as string);
            if (a is double) return Compare(opValue, (double)a, (double)b);

            throw new ApplicationException("unexpected value type");
        }

        bool Compare(string op, string a, string b)
        {
            switch (op)
            {
                case "EQ": return a == b;
                case "NEQ": return a != b;
                case "LT": return string.Compare(a,b) < 0;
                case "LTE": return string.Compare(a,b) <= 0;
                case "GT": return string.Compare(a,b) > 0;
                case "GTE": return string.Compare(a,b) >= 0;
                default: throw new ApplicationException($"Unknown OP {op}");
            }
        }

        bool Compare(string op, double a, double b)
        {
            switch (op)
            {
                case "EQ": return a == b;
                case "NEQ": return a != b;
                case "LT": return a < b;
                case "LTE": return a <= b;
                case "GT": return a > b;
                case "GTE": return a >= b;
                default: throw new ApplicationException($"Unknown OP {op}");
            }
        }

    }

}