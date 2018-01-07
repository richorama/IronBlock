using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextTrim : IBlock
    {
        public override object Evaluate(Context context)
        {
            var mode = this.Fields.Get("MODE");

            var text = (this.Values.Evaluate("TEXT", context) ?? "").ToString();

            switch (mode)
            {
                case "BOTH": return text.Trim();
                case "LEFT": return text.TrimStart();
                case "RIGHT": return text.TrimEnd();
                default: throw new ApplicationException("unknown mode");
            }

        }
    }

}