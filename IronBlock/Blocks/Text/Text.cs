using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextBlock : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var text = this.Fields.Evaluate("TEXT");

            return text;
        }
    }

}