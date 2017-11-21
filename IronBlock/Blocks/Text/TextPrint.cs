using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextPrint : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var text = this.Values.Evaluate("TEXT", variables);

            Console.WriteLine(text);

            return base.Evaluate(variables);
        }
    }

}