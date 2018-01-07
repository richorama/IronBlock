using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextIndexOf : IBlock
    {
        public override object Evaluate(Context context)
        {
            var mode = this.Fields.Get("END");

            var text = (this.Values.Evaluate("VALUE", context) ?? "").ToString();
            var term = (this.Values.Evaluate("FIND", context) ?? "").ToString();

            switch (mode)
            {
                case "FIRST": return text.IndexOf(term) + 1;
                case "LAST": return text.LastIndexOf(term) + 1;
                default: throw new ApplicationException("unknown mode");
            }

        }
    }

}