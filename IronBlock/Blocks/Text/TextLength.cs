using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextLength : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var text = (this.Values.Evaluate("VALUE", variables) ?? "").ToString();

            return text.Length;
        }
    }

}