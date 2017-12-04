using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextTrim : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var mode = this.Fields.Evaluate("MODE");

            var text = (this.Values.Evaluate("TEXT", variables) ?? "").ToString();

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