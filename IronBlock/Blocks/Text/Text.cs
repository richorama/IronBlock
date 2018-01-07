using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextBlock : IBlock
    {
        public override object Evaluate(Context context)
        {
            var text = this.Fields.Get("TEXT");

            return text;
        }
    }

}