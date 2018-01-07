using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextLength : IBlock
    {
        public override object Evaluate(Context context)
        {
            var text = (this.Values.Evaluate("VALUE", context) ?? "").ToString();

            return text.Length;
        }
    }

}