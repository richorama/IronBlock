using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextPrint : IBlock
    {
        public override object Evaluate(Context context)
        {
            var text = this.Values.Evaluate("TEXT", context);

            Console.WriteLine(text);

            return base.Evaluate(context);
        }
    }

}