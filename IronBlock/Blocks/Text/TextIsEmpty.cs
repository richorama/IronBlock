using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextIsEmpty : IBlock
    {
        public override object Evaluate(Context context)
        {
            var text = (this.Values.Evaluate("VALUE", context) ?? "").ToString();

            return string.IsNullOrEmpty(text);
        }
    }

}